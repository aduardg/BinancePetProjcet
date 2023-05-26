using DAL.Context;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceServ.Services
{
    public class BinanceHostService : IHostedService
    {
        public ILogger _logger;
        public ApplicationContext _context;

        public BinanceHostService(ILogger logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            
        }
    }
}
