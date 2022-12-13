using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Telegram_Bot.models
{
    [Table("Users", Schema = "Telegram_Bot")]
    public class User
    {
        [Key]
        public long ChatId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Login { get; set; }

        public User(long ChatId, string? FirstName, string? LastName, string? Login)
        {
            this.ChatId = ChatId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Login = Login;
        }
    }
}
