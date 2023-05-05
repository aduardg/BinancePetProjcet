﻿using BinanceJob.Jobs;
using Domain.Interfaces;
using Quartz;
using Quartz.Impl;

namespace BinanceJob.Services
{
    public class SchedulerWorkService:IWorkService
    {
        private readonly IScheduler _scheduler;
        public SchedulerWorkService(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }
        public async Task Run()
        {

           /* StdSchedulerFactory factory = new StdSchedulerFactory();
            var scheduler = await factory.GetScheduler();*/

            var middleStatistic = GetMiddleStatistic();

            await _scheduler.Start();

            await _scheduler.ScheduleJob(middleStatistic.Item1, middleStatistic.Item2);
        }

        private  (IJobDetail,ITrigger) GetMiddleStatistic()
        {
            var job = JobBuilder.Create<HelloAppJob>()
               .WithIdentity("myJob", "group1")
               .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                .WithCronSchedule("0 0 */1 * * ?")
                //.WithCronSchedule("*/15 * * * * ?")
                .Build();

            return (job, trigger);
        }
    }
}