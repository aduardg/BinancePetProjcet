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
            IList<ValueName> valueNames = await _context.Set<ValueName>().Where(x => x.isActive == true).ToListAsync();

            foreach (ValueName valueName in valueNames)
            {
                var element = await _context.Set<MiddleStatsEntity>()
                    .Where(x => x.namePart == valueName.Name).FirstOrDefaultAsync();

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

                        var sum = elementsToResult.Sum(x => {
                            //Console.WriteLine(x.quoteQty.Replace(".", ","));
                            //return double.Parse(x.quoteQty.Replace(".", ","));
                            return double.Parse(x.quoteQty, CultureInfo.InvariantCulture);
                            });
                        _logger.Information($"{sum}");

                        element.middleStatistic = (element.middleStatistic + sum) / 2;
                        element.dateCreate = DateTime.Now;

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
                        });                        

                        await _context.SaveChangesAsync();
                    }
                }

                catch (Exception ex)
                {
                    _logger.Fatal("Возникло исключение при добавлении в БД \n" +
                        $"Ошибка: {ex.Message}");
                }

            }
        }
    }
}
