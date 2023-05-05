using DAL.Context;
using DAL.Repository;
using Domain.Entity.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BinanceJob.Services
{
    public class BaseWorkService:IWorkService
    {
        ApiClient _apiClient;
        IGenericRepository<TradeElement> _repo;
        ILogger _logger;
        ApplicationContext _context;

        public BaseWorkService(ApiClient apiClient, IGenericRepository<TradeElement> repo,
            ILogger logger, ApplicationContext context)
        {
            _apiClient = apiClient;
            _repo = repo;
            _logger = logger.ForContext<BaseWorkService>();
            _context = context;
        }
        public async Task Run()
        {
            while (true)
            {
                List<ValueName> valueNames = await _context.Set<ValueName>()
                    .Where(x => x.isActive == true).ToListAsync();

                foreach(ValueName valueName in valueNames)
                {
                    var res = await _apiClient.GetData(valueName.Name);
                    await SendToBDResult(res, valueName.Name);
                }

                _logger.Information("Сон на 10 секунд");
                await Task.Delay(10000);
            }

            /*var res = await _apiClient.GetData("LTCUSDT");
            await SendToBDResult(res, "LTCUSDT");
            
            await Console.Out.WriteLineAsync("Ру");*/
        }

        /// <summary>
        /// Method which do write to db
        /// </summary>
        /// <param name="result">result how be parse for model</param>
        /// <param name="namePart">name part currency</param>
        /// <returns>Task result success</returns>
        private async Task SendToBDResult(string ? result, string namePart)
        {
            if (result == null)
            {
                _logger.Warning($"Результат был Null для {namePart}");
            }
            else
            {
                List<TradeElement> tradeElements = JsonSerializer.Deserialize<List<TradeElement>>(result)
                    ?? new List<TradeElement>();

                foreach (var element in tradeElements)
                {
                    try
                    {
                        var repoElement = await _repo.FindById(element.id);
                        if (repoElement is null)
                        {
                            element.namePart = namePart;
                            await _repo.Create(element);
                        }
                    }
                    catch(Exception ex)
                    {
                        _logger.Fatal($"Произошла ошибка с записью \n Текст: {ex.Message}");
                    }
                    
                }

                _logger.Information($"Запись {namePart} прошла успешно");
            }
        }
    }
}
