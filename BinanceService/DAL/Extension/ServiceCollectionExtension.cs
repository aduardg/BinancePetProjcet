using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DAL.Context;

namespace DAL.Extension
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDbModuleWhichExtensions(this IServiceCollection services)
        {
            var envConnection = Environment.GetEnvironmentVariable("DatabaseConnection") ??
                "Host=localhost;Port=5000;Database=BinanceProject;Username=postgres;Password=postgres";

            services.AddDbContext<ApplicationContext>(options =>
            
                options.UseNpgsql(envConnection,
                opt => opt.MigrationsAssembly("DAL"))
            );

            /*Initial Migrate if not any migrate*/
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<ApplicationContext>();
            dbContext.Database.SetConnectionString(envConnection);
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
                Console.WriteLine("Migrate complete...");
            }


            return services;
        }
    }
}
