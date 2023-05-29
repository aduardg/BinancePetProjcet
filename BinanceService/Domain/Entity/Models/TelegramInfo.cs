using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Models
{
    [Table("TelegramInfo")]
    public class TelegramInfo:BaseEntity
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public long? ChatId { get; set; }
        public string? UserName { get; set; }
    }
}
