using DAL.Context;
using Domain.Entity.Enums;
using Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceJob.Jobs
{
    public class CountTransactionJob : IJob
    {
        ILogger _logger;
        ApplicationContext _context;
        public CountTransactionJob(ILogger logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await GoJob();
        }

        public async Task GoJob()
        {
            var valueNames = await _context.valueNames.AsNoTracking().Where(e => e.isActive).ToListAsync();

            foreach (var valueName in valueNames)
            {
                var countElementParts = _context.tradeElements.AsNoTracking().Where(e => e.namePart == valueName.Name).ToList()
                    .Where(e => e.time >= DateTime.UtcNow.AddHours(-1) && e.time <= DateTime.UtcNow).Count();

                var statisticElement = await _context.middleStatsEntities
                    .Where(e => e.namePart == valueName.Name && e.nameStatistic == NameStatisticEnum.CountTransactionStatistic.ToString())
                    .FirstOrDefaultAsync();

                if(statisticElement is null)
                {
                    _logger.Information("Начинаю формирование статистики по количеству транзакций");

                    MiddleStatsEntity entity = new()
                    {
                        dateCreate = DateTime.Now,
                        middleStatistic = countElementParts,
                        namePart = valueName.Name,
                        nameStatistic = NameStatisticEnum.CountTransactionStatistic.ToString(),
                    };

                    await _context.AddAsync(entity);
                    await _context.SaveChangesAsync();
                }

                else
                {
                    statisticElement.middleStatistic = (statisticElement.middleStatistic + countElementParts) / 2;

                    _context.middleStatsEntities.Update(statisticElement);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
