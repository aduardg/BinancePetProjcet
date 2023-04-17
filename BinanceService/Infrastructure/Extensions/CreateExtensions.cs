using Microsoft.Extensions.DependencyInjection;
using DAL.Extension;

namespace Infrastructure.Extensions
{
    public static class CreateExtensions
    {
        public static IServiceCollection AddServicesExtensionBase(this IServiceCollection services)
        {
            services.AddDbModuleWhichExtensions();

            return services;
        }
    }
}
