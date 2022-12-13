using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinanceJob.Models;
using BinanceJob.DataBase;

namespace BinanceJob.Mappers
{
    public class TradeElementMapper
    {
        public static bool updateDatabaseFromList(List<TradeElement> tradeElements)
        {
            using (DbTransaction db = new DbTransaction())
            {
                try
                {                    
                    foreach(TradeElement element in tradeElements)
                    {
                        if(db.tradeElements.FirstOrDefault(elem => elem.id == element.id) is null ? true : false)
                        {
                            db.tradeElements.Add(element);


                            db.SaveChanges();
                        }
                    }                    
                    
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }
    }
}
