using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Extensions;

namespace BinanceService
{
    class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddServicesExtensionBase();
            var sp = services.BuildServiceProvider();

            Console.WriteLine("Успех");
        }
    }
}