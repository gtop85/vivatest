using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vivatest.models;

namespace vivatest.DAL
{
    public interface IFinancialRecordRepository
    {
        Task<List<FinancialRecords>> GetRecordsAsync(ProductDMO searchQuery);
        Task<Guid> UpsertAsync(FinancialRecords financialRecord);
        Task<bool> DeleteRecordAsync(Guid id);
        Task<List<FinancialRecords>> GetRecordsWithinDatesAsync(DateTime dateFrom, DateTime dateTo);
    }
}
