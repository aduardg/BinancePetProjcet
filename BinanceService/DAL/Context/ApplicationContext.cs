using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Domain.Entity.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Design;

namespace DAL.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<TradeElement> tradeElements { get; set; }
        public DbSet<ValueName> valueNames { get; set; }
        public DbSet<MiddleStatsEntity> middleStatsEntities { get; set; }
        public ApplicationContext(DbContextOptions options)
            :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasDefaultSchema("Admin");
            base.OnModelCreating(builder);
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var envConnection = Environment.GetEnvironmentVariable("DatabaseConnection") ??
                "Host=localhost;Port=5000;Database=BinanceProject;Username=postgres;Password=postgres";

            optionsBuilder.UseNpgsql(envConnection);
            
            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}
