using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.SaveModels
{
    [Table("SaveMiddleStatistic", Schema = "Binance")]
    public class SaveMiddleStatsEntities:BaseEntity
    {
        public double middleStatistic { get; set; }
        public string namePart { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime dateCreate { get; set; }
        public string? nameStatistic { get; set; }
    }
}
