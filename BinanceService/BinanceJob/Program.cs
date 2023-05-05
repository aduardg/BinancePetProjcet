using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Extensions;
using Microsoft.Extensions.Hosting;
using Serilog;
using BinanceJob.Services;
using Domain.Interfaces;
using DAL.Repository;
using Quartz;
using BinanceJob.Jobs;
using Quartz.Impl;
using BinanceJob;

namespace BinanceService
{
    class Program
    {
        public static async Task Main(string[] args)
        {

            var services = new ServiceCollection()
                .AddServicesExtensionBase()
                .AddSerilogService(
                    new LoggerConfiguration().WriteTo.Console()
                )
                .AddTransient<ApiClient>()
                .AddTransient<BaseWorkService>()
                .AddTransient(typeof(IGenericRepository<>), typeof(BinanceGenericRepository<>))
                .AddTransient<SchedulerWorkService>();

            var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
            scheduler.JobFactory = new MyJobFactory(services.BuildServiceProvider());

            services.AddSingleton(scheduler);
            var servicesBuilder = services.BuildServiceProvider();

            var workBase = servicesBuilder.GetService<BaseWorkService>();
            workBase?.Run();

            var workScheduler = servicesBuilder.GetService<SchedulerWorkService>();
            await workScheduler?.Run();

            Console.Read();
        }
    }
}