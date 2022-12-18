using BinanceJob.DataBase;

namespace Telegram_Bot.services
{
    public class EventsService
    {
        public static (bool,string) CheckEventsForUser()
        {
            (bool, string) result = new(true, "");

            using (DbTransaction db = new DbTransaction())
            {
                var resultText = (from text in db.tradeElements
                                  where text.namePart == "TRXUSDT"
                                  orderby text.id descending
                                  select text).First();
                result.Item2 = $"Валюта: TRXUSDT | Цена: {resultText.price}";
            }

            return result;
        }
    }
}
