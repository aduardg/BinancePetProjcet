using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceJob.Services.TaskService
{
    public class BinanceTaskService:ITaskService
    {
        
        protected override void InitialTimer(ExecutableMethod method, int seconds)
        {
            _timer = new Timer(
                e => method(),
                null,
                TimeSpan.Zero,
                TimeSpan.FromSeconds(seconds));
        }

        public BinanceTaskService(ExecutableMethod method, int seconds):base(method, seconds)
        {
            
        }

        public BinanceTaskService(Timer timer):base(timer)
        {

        }
    }
}
