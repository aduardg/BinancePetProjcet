using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramService.Quartz.JobFactory
{
    public class JobFactory : IJobFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public JobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return ActivatorUtilities.CreateInstance(_serviceProvider, bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            if (job is IDisposable disposableJob)
                disposableJob.Dispose();
        }
    }
}
