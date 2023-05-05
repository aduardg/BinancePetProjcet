using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceJob.Services
{
    public class StatisticService:IWorkService
    {
        public async Task Run()
        {
            while (true)
            {

                await Task.Delay(3600000);
            }
        }
    }
}
