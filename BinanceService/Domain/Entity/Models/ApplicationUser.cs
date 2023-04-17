using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Access_token { get; set; }
        public string? Refresh_token { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime? LifeRefreshToken { get; set; } 
    }
}
