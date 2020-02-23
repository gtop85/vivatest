using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vivatest.DAL;
using vivatest.fixer;
using vivatest.models;

namespace vivatest.services
{
    public class FinancialRecordsService : IFinancialRecordsService
    {
        readonly IFinancialRecordRepository RecordRepository;
        readonly IProductRepository ProductRepository;
        readonly IFixerService FixerService;

        public FinancialRecordsService(
            IFinancialRecordRepository recordRepository,
            IProductRepository productRepository,
            IFixerService fixerService)
        {
            RecordRepository = recordRepository;
            ProductRepository = productRepository;
            FixerService = fixerService;
        }

        public async Task<CreateResponse> CreateRecordsAsync(CreateRequest request)
        {
            int initialRecords = request.Records.Count();
            var response = new CreateResponse { NewRecords = new List<CreatedRecord>() };

            //Filter out not valid records in request and return if none is left
            request.Records.RemoveAll(x => ServiceExtensions.CheckInvalidProperties(x));
            if (request.Records.Count == 0)
            {
                response.RecordsFailed = initialRecords;
                return response;
            }

            //Get the current EURO currency from exchangeratesapi.io (Used instead of fixer due to restricted access)
            var ratesResponse = await FixerService.GetLatestRatesAsync();
            var currency = ratesResponse.Rates.GetValueOrDefault("EUR");

            //Execute database inserts in parallel async
            List<Task<CreatedRecord>> tasks = new List<Task<CreatedRecord>>();
            foreach (var dataRecord in request.Records)
            {
                tasks.Add(CreateNewRecordAsync(currency, dataRecord));
            }
            var recordsInserted = await Task.WhenAll(tasks);

            //Populate response
            return new CreateResponse
            {
                NewRecords = recordsInserted.ToList(),
                RecordsCreated = recordsInserted.Length,
                RecordsFailed = initialRecords - recordsInserted.Length
            };
        }


        public async Task<List<CreatedRecord>> SearchRecordsAsync(ProductDMO searchQuery)
        {
            //Find records
            List<FinancialRecords> financialRecords = await RecordRepository.GetRecordsAsync(searchQuery);

            //Get record details in parallel async
            List<Task<CreatedRecord>> tasks = new List<Task<CreatedRecord>>();
            foreach (FinancialRecords financialRecord in financialRecords)
            {
                tasks.Add(GetRecordDetailsAsync(financialRecord));
            }
            var result = await Task.WhenAll(tasks);
            return result.ToList();
        }

        public async Task<bool> UpdateRecordAsync(Guid id, Record record)
        {
            if (ServiceExtensions.CheckInvalidProperties(record))
                return false;

            //check if record exists, return if not
            var result = await RecordRepository.GetRecordsAsync(new ProductDMO { Id = id });
            if (result.Count == 0) return false;

            Guid productId = await ProductRepository.GetProductIdAsync(record);

            //Get the current EURO currency from exchangeratesapi.io (Used instead of fixer due to restricted access)
            var ratesResponse = await FixerService.GetLatestRatesAsync();
            var currency = ratesResponse.Rates.GetValueOrDefault("EUR");
            //calculate sales, banker's rounding
            decimal? salesInEuro = CalculateSalesInEuro(currency, record);
            FinancialRecords financialRecord = record.ToFinancialRecord(productId, salesInEuro);
            financialRecord.Id = id;
            return await RecordRepository.UpsertAsync(financialRecord) != Guid.Empty;
        }

        public async Task<bool> DeleteRecordAsync(Guid id)
        {
            return await RecordRepository.DeleteRecordAsync(id);
        }

        public async Task<List<Report>> GetReportAsync(DateTime dateFrom, DateTime dateTo)
        {
            List<Report> reports = new List<Report>();
            List<FinancialRecords> financialRecords = await RecordRepository.GetRecordsWithinDatesAsync(dateFrom, dateTo);
            if (financialRecords.Count == 0) return reports;

            //Get reports in parallel async
            List<Task<Report>> tasks = new List<Task<Report>>();
            foreach (FinancialRecords financialRecord in financialRecords)
            {
                tasks.Add(GetProductDetailsAsync(financialRecord));
            }
            var results = await Task.WhenAll(tasks);
            reports = results.ToList();

            //Calculate statistics 
            var statsList = reports.GroupBy(x => new { x.Segment, x.Country })
                                   .Select(z =>
                                   {
                                       var statResults = z.Aggregate(new ReportStatistics(),
                                           (acc, x) => acc.Accumulate(x),
                                           acc => acc.Compute());
                                       return new
                                       {
                                           Segment = z.Key.Segment,
                                           Country = z.Key.Country,
                                           averageSalePrice = statResults.AverageSalePrice,
                                           grossSalesSum = statResults.GrossSalesSum,
                                           maxProfit = statResults.MaxProfit
                                       };
                                   }).ToList();

            // Insert stats to report
            foreach (var report in reports)
            {
                var stats = statsList.FirstOrDefault(x => x.Country == report.Country && x.Segment == report.Segment);
                report.AverageSalePrice = decimal.Round(stats.averageSalePrice, 2, MidpointRounding.ToEven);
                report.GrossSalesSum = stats.grossSalesSum;
                report.MaxProfit = stats.maxProfit;
            }
            return reports;
        }

        private async Task<Report> GetProductDetailsAsync(FinancialRecords financialRecord)
        {
            var record = financialRecord.ToRecord();
            var report = new Report(record);
            var product = await ProductRepository.GetProductByProductIdAsync(financialRecord.ProductId);
            report.Id = product?.Id;
            report.Product = product?.Product;
            report.Segment = product?.Segment;
            report.Country = product?.Country;
            report.DiscountBand = product?.DiscountBand;
            return report;
        }

        private async Task<CreatedRecord> GetRecordDetailsAsync(FinancialRecords financialRecord)
        {
            Record rec = financialRecord.ToRecord();
            CreatedRecord record = new CreatedRecord(rec)
            {
                Id = financialRecord.Id,
                SalesInEuro = financialRecord.SalesInEuro
            };
            var product = await ProductRepository.GetProductByProductIdAsync(financialRecord.ProductId);
            record.Product = product?.Product;
            record.Segment = product?.Segment;
            record.Country = product?.Country;
            record.DiscountBand = product?.DiscountBand;
            return record;
        }

        private async Task<CreatedRecord> CreateNewRecordAsync(decimal currency, Record dataRecord)
        {
            Guid productId = await ProductRepository.GetProductIdAsync(dataRecord);
            //calculate sales, banker's rounding
            decimal? salesInEuro = CalculateSalesInEuro(currency, dataRecord);
            FinancialRecords financialRecord = dataRecord.ToFinancialRecord(productId, salesInEuro);

            Guid Id = await RecordRepository.UpsertAsync(financialRecord);
            if (Id != null && Id != Guid.Empty)
            {
                CreatedRecord createdRecord = new CreatedRecord(dataRecord)
                {
                    SalesInEuro = salesInEuro,
                    Id = Id
                };
                return createdRecord;
            }
            return null;
        }

        private static decimal? CalculateSalesInEuro(decimal currency, Record dataRecord)
        {
            decimal? salesInEuro = dataRecord.Sales * currency;
            salesInEuro = decimal.Round(salesInEuro.Value, 2, MidpointRounding.ToEven);
            return salesInEuro;
        }
    }
}
