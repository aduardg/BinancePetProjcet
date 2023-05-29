using Domain.Interfaces;
using Quartz;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramService.Quartz.Job;

namespace TelegramService.Services
{
    public class JobService:IWorkService
    {
        private readonly IScheduler _scheduler;
        private ILogger _logger;

        public JobService(IScheduler scheduler, ILogger logger)
        {
            _scheduler = scheduler;
            _logger = logger;
        }

        public async Task Run()
        {
            var jobTrigger = GetJobTriggerDetails();

            await _scheduler.Start();
            _logger.Information("Началось выполнение работ");
            await _scheduler.ScheduleJob(jobTrigger.Item1, jobTrigger.Item2);
        }

        private (IJobDetail, ITrigger) GetJobTriggerDetails()
        {
            var job = JobBuilder.Create<JobSendTenMinutes>()
               .WithIdentity("JobTenMinutes", "group2")
               .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("JobTenTrigger", "group2")
                .WithCronSchedule("0 */10 * * * ?")
                //.WithCronSchedule("*/15 * * * * ?")
                .Build();

            return (job, trigger);
        }

    }
}
