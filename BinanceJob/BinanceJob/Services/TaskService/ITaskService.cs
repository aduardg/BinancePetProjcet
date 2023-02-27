using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceJob.Services.TaskService
{
    public abstract class ITaskService
    {
        private protected Timer _timer;
        public delegate void ExecutableMethod();

        protected virtual void InitialTimer(ExecutableMethod method,int seconds) { }
        public void DisposeTimer() 
        { 
            _timer.Dispose(); 
        }
        public ITaskService(ExecutableMethod method,int seconds)
        {
            InitialTimer(method,seconds);
        }

        public ITaskService(Timer timer)
        {
            this._timer = timer;
        }
        
    }
}
