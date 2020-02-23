using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using vivatest.models;

namespace vivatest.DAL
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration Configuration;

        public ProductRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(Configuration.GetConnectionString("ConnString"));
        }

        public async Task<ProductDMO> GetProductByProductIdAsync(Guid productId)
        {
            using (var con = GetConnection())
            {
                var result = await con.QueryAsync<ProductDMO>("GetProductByProductId", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }

        public async Task<Guid> GetProductIdAsync(Record record)
        {
            using (var con = GetConnection())
            {
                var result = await con.QueryAsync<Guid>("GetProductId", new
                {
                    Product = record.Product,
                    Segment = record.Segment,
                    Country = record.Country,
                    DiscountBand = record.DiscountBand
                }, commandType: CommandType.StoredProcedure);
                return result.FirstOrDefault();
            }
        }
    }
}
