using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Models
{
    public class TradeElementView:BaseEntity
    {
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

        public TradeElementView(int id, string? price, string? qty, string? quoteQty, long time, bool isBuyerMaker, bool isBestMatch, string? namePart)
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
