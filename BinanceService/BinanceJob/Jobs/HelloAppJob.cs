using DAL.Context;
using Domain.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Domain.Entity.Enums;
using Domain.Entity.SaveModels;

namespace BinanceJob.Jobs
{
    public class HelloAppJob : IJob
    {
        ApplicationContext _context;
        ILogger _logger;

        public HelloAppJob(ApplicationContext context, ILogger logger)
        {
            _context = context;
            _logger = logger.ForContext<HelloAppJob>();
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await GoJobCountStatistic();
            await GoJobValueStatistic();                        
        }

        public async Task GoJobValueStatistic()
        {
            IList<ValueName> valueNames = await _context.Set<ValueName>().Where(x => x.isActive == true).ToListAsync();

            foreach (ValueName valueName in valueNames)
            {
                var element = await _context.Set<MiddleStatsEntity>()
                    .Where(x => x.namePart == valueName.Name && x.nameStatistic == NameStatisticEnum.VolumeStatistic.ToString())
                    .FirstOrDefaultAsync();

                try
                {
                    if (element != null)
                    {
                        var elementsFromTime = await _context.tradeElements
                            .Where(x => x.namePart == valueName.Name).OrderBy(x => x.time).ToListAsync();


                        var elementsToResult = elementsFromTime.Where(x =>
                        DateTime.UtcNow.AddHours(-1) <= x.time &&
                        x.time <= DateTime.UtcNow).OrderByDescending(x => x.time).ToList();

                        _logger.Information($"Начинаю формирование статистики для валютной пары {valueName.Name}");
                        _logger.Information($"{DateTimeOffset.UtcNow.AddHours(-1)}");

                        var sum = elementsToResult.Sum(x =>
                        {
                            //Console.WriteLine(x.quoteQty.Replace(".", ","));
                            //return double.Parse(x.quoteQty.Replace(".", ","));
                            return double.Parse(x.quoteQty, CultureInfo.InvariantCulture);
                        });

                        SaveMiddleStatsEntities saveStatisticElement = new()
                        {
                            dateCreate = DateTime.Now,
                            middleStatistic = sum,
                            namePart = valueName.Name,
                            nameStatistic = NameStatisticEnum.VolumeStatistic.ToString()
                        };

                        element.middleStatistic = (element.middleStatistic + sum) / 2;
                        element.dateCreate = DateTime.Now;

                        await _context.AddAsync(saveStatisticElement);
                        await _context.SaveChangesAsync();
                    }

                    else
                    {
                        _logger.Information($"Валютной пары {valueName.Name} еще не было\n" +
                            $"Начинаю производить запись");

                        var elementsFromTime = await _context.tradeElements
                            .Where(x => x.namePart == valueName.Name).OrderBy(x => x.time).ToListAsync();


                        var elementsToResult = elementsFromTime.Where(x =>
                        DateTime.UtcNow.AddHours(-1) <= x.time &&
                        x.time <= DateTime.UtcNow).ToList();

                        var sum = elementsToResult.Sum(x => double.Parse(x.quoteQty, CultureInfo.InvariantCulture));
                        _logger.Information($"{sum}");

                        await _context.middleStatsEntities.AddAsync(new MiddleStatsEntity()
                        {
                            dateCreate = DateTime.Now,
                            middleStatistic = sum,
                            namePart = valueName.Name,
                            nameStatistic = NameStatisticEnum.VolumeStatistic.ToString(),
                        });

                        SaveMiddleStatsEntities saveStatisticElement = new()
                        {
                            dateCreate = DateTime.Now,
                            middleStatistic = sum,
                            namePart = valueName.Name,
                            nameStatistic = NameStatisticEnum.VolumeStatistic.ToString()
                        };

                        await _context.AddAsync(saveStatisticElement);
                        await _context.SaveChangesAsync();
                    }
                }

                catch (Exception ex)
                {
                    _logger.Fatal("Возникло исключение при добавлении в БД \n" +
                        $"Ошибка: {ex.Message}");
                }

                finally
                {
                    _context.ChangeTracker.Clear();
                }
            }
        }
        public async Task GoJobCountStatistic()
        {
            var valueNames = await _context.valueNames.AsNoTracking().Where(e => e.isActive).ToListAsync();

            foreach (var valueName in valueNames)
            {
                var countElementParts = _context.tradeElements.AsNoTracking().Where(e => e.namePart == valueName.Name).ToList()
                    .Where(e => e.time >= DateTime.UtcNow.AddHours(-1) && e.time <= DateTime.UtcNow).Count();

                var statisticElement = await _context.middleStatsEntities
                    .Where(e => e.namePart == valueName.Name && e.nameStatistic == NameStatisticEnum.CountTransactionStatistic.ToString())
                    .FirstOrDefaultAsync();

                if (statisticElement is null)
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
                    
                }

                else
                {
                    statisticElement.middleStatistic = (statisticElement.middleStatistic + countElementParts) / 2;

                    _context.middleStatsEntities.Update(statisticElement);
                }

                SaveMiddleStatsEntities saveStatisticElement = new()
                {
                    dateCreate = DateTime.Now,
                    middleStatistic = countElementParts,
                    namePart = valueName.Name,
                    nameStatistic = NameStatisticEnum.CountTransactionStatistic.ToString()
                };

                await _context.AddAsync(saveStatisticElement);
                await _context.SaveChangesAsync();
            }
        }
    }
}
