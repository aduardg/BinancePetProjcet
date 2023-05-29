using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Entity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Access_token { get; set; }
        public string? Refresh_token { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? LifeRefreshToken { get; set; }

        [JsonIgnore]
        public int? TelegramId { get; set; }
        [ForeignKey("TelegramId")]
        public TelegramInfo? TelegramInfo { get; set; }
    }
}
