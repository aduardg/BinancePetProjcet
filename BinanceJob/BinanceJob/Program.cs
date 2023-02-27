using BinanceJob.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Microsoft.Extensions.Configuration;
using BinanceJob.Services.TaskService;

namespace BinanceJob
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await InitMigrate();
            /*await jobController();*/
            BinanceTaskService taskService = new BinanceTaskService(jobController,10);
            Console.ReadKey();
        }

        /*
         * Выполнение определенной задачи
        */
        public static void job(object ? nameVal)
        {                        
           ServiceHttp.getValueofPart((string)nameVal);
        }

        public static void jobController()
        {            
                using (DbTransaction db = new DbTransaction())
                {
                    var result = (from elem in db.valueNames
                                  where elem.isBlocked == false
                                  select elem.Name).ToList();

                    foreach (string names in result)
                    {
                        ThreadPool.QueueUserWorkItem(job, names);
                    }                                                                                   
                }           
        }

        /*
         * Инициализация миграции БД
         */
        public static async Task InitMigrate()
        {
            await Task.Run(() =>
            {
                using (DbTransaction db = new DbTransaction())
                {
                    db.Database.Migrate();
                }
            });
           
        }
    }
}