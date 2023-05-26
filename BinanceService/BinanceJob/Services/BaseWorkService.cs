﻿using AutoMapper;
using DAL.Context;
using DAL.Repository;
using Domain.Entity.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

namespace BinanceJob.Services
{
    public class BaseWorkService:IWorkService
    {
        ApiClient _apiClient;
        IGenericRepository<TradeElement> _repo;
        ILogger _logger;
        ApplicationContext _context;
        IMapper _mapper;

        public BaseWorkService(ApiClient apiClient, IGenericRepository<TradeElement> repo,
            ILogger logger, ApplicationContext context, IMapper mapper)
        {
            _apiClient = apiClient;
            _repo = repo;
            _logger = logger.ForContext<BaseWorkService>();
            _context = context;
            _mapper = mapper;
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

                _logger.Information("Сон на 4 секундs");
                await Task.Delay(4000);
            }
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
                List<TradeElementView> tradeElements = JsonSerializer.Deserialize<List<TradeElementView>>(result)
                    ?? new List<TradeElementView>();

                try
                {                   
                    foreach(var item in tradeElements)
                        item.namePart = namePart;

                    var temp = _mapper.Map<IEnumerable<TradeElement>>(tradeElements.Where(e => _context.tradeElements.Find(e.id) == null));
                    await _context.AddRangeAsync(temp);
                    await _context.SaveChangesAsync();
                    _logger.Information($"Запись {namePart} прошла успешно");
                    _context.ChangeTracker.Clear();
                }
                catch(Exception ex)
                {
                    _logger.Fatal($"Произошла ошибка с записью \n Текст: {ex.Message}");
                }
                /*foreach (var element in tradeElements)
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
                    
                }*/

                
            }
        }
    }
}
