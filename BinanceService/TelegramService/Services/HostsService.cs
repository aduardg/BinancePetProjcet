using Domain.Entity.Models;
using Domain.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Hosting;
using Serilog;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramService.Services
{
    public class HostsService : IHostedService
    {
        private ILogger _logger;
        private IWorkService _workService;
        private readonly TelegramBotService _telegramBotService;
        public HostsService(ILogger logger, IWorkService workService
            ,TelegramBotService telegramBotService
            )
        {
            _logger = logger;
            _workService = workService;
            _telegramBotService = telegramBotService;

        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Сервис начал работу");

            _workService.Run();
            _telegramBotService.StartTelegramBot();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Сервис закончил работу");
        }       
    }
}
