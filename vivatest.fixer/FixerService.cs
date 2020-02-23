using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace vivatest.fixer
{
    public class FixerService : IFixerService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly FixerSettings fixerSettings;

        public FixerService(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            fixerSettings = configuration.GetSection("FixerSettings").Get<FixerSettings>();
        }
        public async Task<RatesResponse> GetLatestRatesAsync()
        {
            var response = new RatesResponse(); 

            var client = _clientFactory.CreateClient();
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                //{ "access_key", fixerSettings.ApiKey },
                { "base", fixerSettings.BaseCurrency },
                //{ "rates", fixerSettings.Rates }
                { "symbols", fixerSettings.Rates }
            };
            var requestUri = QueryHelpers.AddQueryString(fixerSettings.BaseUrl + "latest", parameters);
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var httpResponse = await client.SendAsync(request);
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseStream = await httpResponse.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<RatesResponse>(responseStream);
            } 
            return response;
        }
    }
}
