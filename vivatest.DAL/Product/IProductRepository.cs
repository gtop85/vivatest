using System;
using System.Threading.Tasks;
using vivatest.models;

namespace vivatest.DAL
{
    public interface IProductRepository
    {
        Task<ProductDMO> GetProductByProductIdAsync(Guid productId);
        Task<Guid> GetProductIdAsync(Record record);
    }
}
