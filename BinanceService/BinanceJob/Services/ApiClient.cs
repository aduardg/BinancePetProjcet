using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace BinanceJob.Services
{
    public class ApiClient
    {
        ILogger _logger;
        IHttpClientFactory _httpClietFactory;
        public ApiClient(ILogger logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger.ForContext<ApiClient>();
            _httpClietFactory = httpClientFactory;
        }

        public async Task<string?> GetData(string currencyPair)
        {
            try
            {
                var client = _httpClietFactory.CreateClient();
                var result = await client.GetStringAsync($"https://api.binance.com/api/v3/trades?symbol={currencyPair}&limit=500");
              
                return result;
            }

            catch(Exception ex)
            {
                _logger.Fatal($"Произошла ошибка с запросом : {ex.Message}");
                return null;
            }
        }
    }
}
