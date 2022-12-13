using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BinanceJob.Models
{
    [Table("TradeElements", Schema = "Binance")]
    public class TradeElement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? price { get; set; }
        public string? qty { get; set; }
        public string? quoteQty { get; set; }
        public long time { get; set; }

        //false - покупка
        //true - продажа
        public bool isBuyerMaker { get; set; }
        public bool isBestMatch { get; set; }
        public string? namePart { get; set; }
        public string? checkColumn { get; set; }

        public TradeElement(int id, string? price, string? qty, string? quoteQty, long time, bool isBuyerMaker, bool isBestMatch, string? namePart)
        {
            this.id = id;
            this.price = price;
            this.qty = qty;
            this.quoteQty = quoteQty;
            this.time = time;
            this.isBuyerMaker = isBuyerMaker;
            this.isBestMatch = isBestMatch;
            this.namePart = namePart;
        }

        public override string ToString()
        {
            return $"{id}\n" +
                $"{price}\n" +
                $"{qty}\n" +
                $"{quoteQty}\n" +
                $"{time}\n" +
                $"{isBuyerMaker}\n" +
                $"{isBestMatch}\n" +
                $"{namePart}";
        }
    }
}
