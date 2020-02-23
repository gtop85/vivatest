using System;
using vivatest.models;

namespace vivatest.services
{
    public static class ServiceExtensions
    {
        //public static bool CheckAndFilterRecordsToInsert(CreateRequest request)
        //{
        //    request.Records.RemoveAll(x => CheckValidProperties(x));
        //}

        ////public static void CheckAndFilterRecordToInsert(Record record)
        ////{
        ////    CheckNullProperties()

        ////    request.Records.RemoveAll(x => CheckValidProperties(x));
        ////}

        ////public static bool CheckNullProperties(Record record)
        ////{
        ////    return string.IsNullOrWhiteSpace(record.Product) || string.IsNullOrWhiteSpace(record.Segment) ||
        ////                        string.IsNullOrWhiteSpace(record.Country) || string.IsNullOrWhiteSpace(record.DiscountBand);
        ////}




        /// <summary>
        /// Returns true if the properties are valid
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public static bool CheckInvalidProperties(Record record)
        {
            var supportedProducts = typeof(ProductType).GetConstValues();
            var supportedSegments = typeof(SegmentType).GetConstValues();
            var supportedCountries = typeof(CountryType).GetConstValues();
            var supportedDiscountBands = typeof(DiscountBandType).GetConstValues();

            return !(supportedProducts.Contains(record.Product) && supportedCountries.Contains(record.Country) &&
                     supportedSegments.Contains(record.Segment) && supportedDiscountBands.Contains(record.DiscountBand));
        }

        public static FinancialRecords ToFinancialRecord(this Record record, Guid productId, decimal? salesInEuro)
        {
            return new FinancialRecords
            {
                ProductId = productId,
                UnitsSold = record.UnitsSold,
                ManufacturingPrice = record.ManufacturingPrice,
                GrossSales = record.GrossSales,
                Discounts = record.Discounts,
                SalePrice = record.SalePrice,
                CostOfGoodsSold = record.CostOfGoodsSold,
                Sales = record.Sales,
                SalesInEuro = salesInEuro,
                Profit = record.Profit,
                Date = record.Date
            };
        }

        public static Record ToRecord(this FinancialRecords record)
        {
            return new Record
            {
                UnitsSold = record.UnitsSold,
                ManufacturingPrice = record.ManufacturingPrice,
                GrossSales = record.GrossSales,
                Discounts = record.Discounts,
                SalePrice = record.SalePrice,
                CostOfGoodsSold = record.CostOfGoodsSold,
                Sales = record.Sales,
                Profit = record.Profit,
                Date = record.Date
            };
        }

        public static Record ToRecord(this ProductDMO product, Record record)
        {            
            record.Product = product.Product;
            record.Segment = product.Segment;
            record.Country = product.Country;
            record.DiscountBand = product.DiscountBand;
            return record;
        }

        //public static CreatedRecord ToCreatedRecord(this FinancialRecords record)
        //{
        //    return new CreatedRecord
        //    {
        //        Id = record.Id,
        //        SalesInEuro = record.SalesInEuro,
        //        UnitsSold = record.UnitsSold,
        //        ManufacturingPrice = record.ManufacturingPrice,
        //        GrossSales = record.GrossSales,
        //        Discounts = record.Discounts,
        //        SalePrice = record.SalePrice,
        //        CostOfGoodsSold = record.CostOfGoodsSold,
        //        Sales = record.Sales,
        //        Profit = record.Profit,
        //        Date = record.Date
        //    };
        //}
    }
}


