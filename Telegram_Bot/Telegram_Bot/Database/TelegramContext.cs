using Telegram_Bot.models;
using Microsoft.EntityFrameworkCore;

namespace Telegram_Bot.Database
{
    public class TelegramContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=192.168.1.70;Port=5432;Database=TelegramBase;Username=Eduard;Password=527225");
        }
    }
}
