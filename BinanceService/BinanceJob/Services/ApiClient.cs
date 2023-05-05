using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace BinanceJob.Services
{
    public class ApiClient
    {
        ILogger _logger;
        public ApiClient(ILogger logger)
        {
            _logger = logger.ForContext<ApiClient>();
        }

        public async Task<string?> GetData(string currencyPair)
        {
            try
            {
                var client = new HttpClient();
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
