using DAL.Context;
using Domain.Entity.Enums;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;
using TelegramService.Services;

namespace TelegramService.Quartz.Job
{
    public class JobSendTenMinutes : IJob
    {
        ILogger _logger;
        ApplicationContext _context;
        private readonly TelegramBotService _telegramBotService;
        public JobSendTenMinutes(ILogger logger, ApplicationContext context,
            TelegramBotService telegramBotService)
        {
            _logger = logger;
            _context = context;
            _telegramBotService = telegramBotService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await CheckVolumeStatistics();
        }

        #region Проверка статистики по объему
        /*Проверка статистики по объему*/
        private async Task CheckVolumeStatistics()
        {
            var activeValues = _context.valueNames.Where(element => element.isActive).ToList();

            foreach (var activeValue in activeValues)
            {
                var activeTime = DateTime.UtcNow;

                if (activeTime.Minute <= 40)
                {
                    var elements = _context.tradeElements.Where(element => element.namePart == activeValue.Name)
                        .ToList()
                        .Where(element => element.time <= activeTime && element.time >= DateTime.UtcNow.AddMinutes(-activeTime.Minute)).ToList();

                    var sum = elements.Sum(element => double.Parse(element.quoteQty, CultureInfo.InvariantCulture));

                    var volumeValue = await _context.middleStatsEntities
                        .Where(element => element.namePart == activeValue.Name && element.nameStatistic == NameStatisticEnum.VolumeStatistic.ToString())
                        .FirstOrDefaultAsync();
                    
                    //отправка статистики
                    if (sum >= volumeValue?.middleStatistic * 1.2)
                    {
                        var UsersWaitMessage = await _context.Users.Include(u => u.TelegramInfo)
                            .Where(e => e.TelegramInfo.ChatId != null).ToListAsync();

                        foreach(var user in UsersWaitMessage)
                        {
                            string message = $"На {activeTime.Minute}-ой минуте замечена активность объема по валютной паре {activeValue.Name}";

                            await _telegramBotService.SendMessageTelegramBotAsync(user.TelegramInfo.ChatId ?? 0,message);
                        }
                    }                        

                    _context.ChangeTracker.Clear();
                }
            }
        }
        #endregion

        #region Проверка статистики по числу транзакций
        #endregion
    }
}
