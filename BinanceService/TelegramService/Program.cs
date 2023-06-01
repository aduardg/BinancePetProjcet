using DAL.Extension;
using Domain.Interfaces;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz.Impl;
using Serilog;
using TelegramService.Quartz.JobFactory;
using TelegramService.Services;

namespace TelegramService
{
    class Program
    {
        public static void Main(string[] args)
        {        
            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging(config => config.SetMinimumLevel(LogLevel.Warning))
                .ConfigureServices(services =>
                {
                    services
                    .AddSerilogService(new LoggerConfiguration().WriteTo.Console())
                    .AddHostedService<HostsService>()
                    .AddDbModuleWhichExtensions()
                    .AddTransient<IUserService, UserService>()
                    .AddSingleton<IWorkService, JobService>()
                    .AddSingleton(typeof(TelegramBotService));

                    var scheduler = StdSchedulerFactory.GetDefaultScheduler().GetAwaiter().GetResult();
                    scheduler.JobFactory = new JobFactory(services.BuildServiceProvider());

                    services.AddSingleton(scheduler);
                }).Build();

            
            host.Run();

            Console.Read();
        }        
    }
}