using Newtonsoft.Json.Linq;
using RestSharp;
using BinanceJob.Services.LoggerService;
using BinanceJob.Models;
using BinanceJob.Mappers;

namespace BinanceJob
{
    public class ServiceHttp
    {
        public static void getValueofPart(string namePart)
        {
            BinanceLoggerService logger = new BinanceLoggerService();
            try
            {
                var client = new RestClient($"https://api.binance.com/api/v3/trades?symbol={namePart}&limit=500");
                var request = new RestRequest("", Method.Get);
                RestResponse response = client.Execute(request);


                var responseData = JArray.Parse(response.Content is null ? throw new Exception("Content is null") : response.Content);
                List<TradeElement> tradeList = new List<TradeElement>();
                foreach (var element in responseData)
                {
                    tradeList.Add(new TradeElement(
                        (int)element["id"], (string)element["price"], (string)element["qty"], (string)element["quoteQty"],
                        (long)element["time"], (bool)element["isBuyerMaker"], (bool)element["isBestMatch"], namePart
                        ));
                }

                logger.Info($"Проверка {namePart}: {TradeElementMapper.updateDatabaseFromList(tradeList)}");
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
