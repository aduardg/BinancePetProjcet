using Microsoft.Extensions.DependencyInjection;
using DAL.Extension;
using Serilog;

namespace Infrastructure.Extensions
{
    public static class CreateExtensions
    {
        public static IServiceCollection AddServicesExtensionBase(this IServiceCollection services)
        {
            services.AddDbModuleWhichExtensions();

            return services;
        }

        public static IServiceCollection AddSerilogService(
            this IServiceCollection services,
            LoggerConfiguration configuration)
        {
            Log.Logger = configuration.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }
    }
}
