using System;
using System.Threading.Tasks;

namespace vivatest.fixer
{
    public interface IFixerService
    {
        Task<RatesResponse> GetLatestRatesAsync();
    }
}
