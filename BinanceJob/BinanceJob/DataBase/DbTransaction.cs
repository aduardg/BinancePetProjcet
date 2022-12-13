using Microsoft.EntityFrameworkCore;
using BinanceJob.Models;

namespace BinanceJob.DataBase
{
    public class DbTransaction:DbContext
    {
        readonly string _ServerName = Environment.GetEnvironmentVariable("DATABASE_ADDRESS") is null ? "localhost" : Environment.GetEnvironmentVariable("DATABASE_ADDRESS");
        public DbSet<TradeElement> tradeElements { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Server={_ServerName};Port=5432;Database=BinanceBase;Username=Eduard;Password=527225");
        }
    }
}
