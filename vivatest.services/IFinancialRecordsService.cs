using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vivatest.models;

namespace vivatest.services
{
    public interface IFinancialRecordsService
    {
        Task<CreateResponse> CreateRecordsAsync(CreateRequest request);
        Task<List<CreatedRecord>> SearchRecordsAsync(ProductDMO searchQuery);
        Task<bool> UpdateRecordAsync(Guid id, Record record);
        Task<bool> DeleteRecordAsync(Guid id);
        Task<List<Report>> GetReportAsync(DateTime dateFrom, DateTime dateTo);
    }
}
