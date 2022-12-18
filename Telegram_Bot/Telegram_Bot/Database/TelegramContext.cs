using Telegram_Bot.models;
using Microsoft.EntityFrameworkCore;

namespace Telegram_Bot.Database
{
    public class TelegramContext:DbContext
    {
        readonly string _ServerName = Environment.GetEnvironmentVariable("DATABASE_ADDRESS") is null ? "localhost" : Environment.GetEnvironmentVariable("DATABASE_ADDRESS");
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Server={_ServerName};Port=5432;Database=TelegramBase;Username=Eduard;Password=527225");
        }
    }
}
