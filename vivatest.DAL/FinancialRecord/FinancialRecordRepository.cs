using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using vivatest.models;

namespace vivatest.DAL
{

    public class FinancialRecordRepository : IFinancialRecordRepository
    {
        private readonly IConfiguration Configuration;

        public FinancialRecordRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(Configuration.GetConnectionString("ConnString"));
        }

        public async Task<List<FinancialRecords>> GetRecordsAsync(ProductDMO searchQuery)
        {
            using (var con = GetConnection())
            {
                var results = await con.QueryAsync<FinancialRecords>("GetRecords", searchQuery, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
        }

        public async Task<Guid> UpsertAsync(FinancialRecords financialRecord)
        {
            using (var con = GetConnection())
            {
                var result = await con.QueryAsync<Guid>("Upsert", financialRecord, commandType: CommandType.StoredProcedure);
                return result.SingleOrDefault();
            }
        }

        public async Task<bool> DeleteRecordAsync(Guid id)
        {
            using (var con = GetConnection())
            {
                //returns true if the row is deleted
                return await con.ExecuteAsync("DeleteRecord", new { id }, commandType: CommandType.StoredProcedure) == 1;
            }
        }

        public async Task<List<FinancialRecords>> GetRecordsWithinDatesAsync(DateTime dateFrom, DateTime dateTo)
        {
            using (var con = GetConnection())
            {
                var results = await con.QueryAsync<FinancialRecords>("GetRecordsWithinDates", new { DateFrom = dateFrom, DateTo = dateTo }, commandType: CommandType.StoredProcedure);
                return results.ToList();
            }
        }
    }
}
