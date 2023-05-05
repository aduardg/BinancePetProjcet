using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Models
{
    [Table("ValueNames", Schema = "Binance")]
    public class ValueName:BaseEntity
    {
        public string Name { get; set; }
        public bool isActive { get; set; }
    }
}
