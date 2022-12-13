using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceJob.Models;
using BinanceJob.Mappers;

namespace BinanceJob
{
    public class ServiceHttp
    {
        public static void getValueofPart(string namePart)
        {
            try
            {


                var client = new RestClient($"https://api.binance.com/api/v3/trades?symbol={namePart}&limit=500");
                var request = new RestRequest("", Method.Get);
                RestResponse response = client.Execute(request);


                var responseData = JArray.Parse(response.Content);
                List<TradeElement> tradeList = new List<TradeElement>();
                foreach (var element in responseData)
                {
                    tradeList.Add(new TradeElement(
                        (int)element["id"], (string)element["price"], (string)element["qty"], (string)element["quoteQty"],
                        (long)element["time"], (bool)element["isBuyerMaker"], (bool)element["isBestMatch"], namePart
                        ));
                }

                Console.WriteLine($"Проверка : {TradeElementMapper.updateDatabaseFromList(tradeList)}");

                Console.WriteLine(responseData.Count);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
