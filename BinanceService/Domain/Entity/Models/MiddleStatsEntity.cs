using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Models
{
    [Table("MiddleStatistic", Schema = "Binance")]
    public class MiddleStatsEntity:BaseEntity
    {
        public double middleStatistic { get; set; }
        public string namePart { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime dateCreate { get; set; }
    }
}
