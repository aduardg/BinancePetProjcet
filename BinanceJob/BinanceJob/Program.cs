using BinanceJob.DataBase;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace BinanceJob
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await InitMigrate();
            await jobController();
            Console.ReadKey();
        }

        /*
         * Выполнение определенной задачи
        */
        public static async Task job(string nameVal)
        {
            Console.WriteLine("Начинаю работу");
            Thread t = new Thread(async () =>
            {
                ServiceHttp.getValueofPart(nameVal);
                Console.WriteLine("Закончил работу");
            });
            t.Start();
        }

        public static async Task jobController()
        {
            while (true)
            {
                using (DbTransaction db = new DbTransaction())
                {
                    var result = (from elem in db.valueNames
                                  where elem.isBlocked == false
                                  select elem.Name).ToList();
                    foreach (string names in result)
                         job(names);
                    Thread.Sleep(20000);

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