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
            Console.WriteLine("1");
            await InitMigrate();
            Console.WriteLine("3");
            while (true)
            {
                job();
                Thread.Sleep(20000);
            }
            Console.ReadKey();
        }

        /*
         * Выполнение определенной задачи
        */
        public static async Task job()
        {
            Console.WriteLine("Начинаю работу");
            Thread t = new Thread(async () =>
            {
                ServiceHttp.getValueofPart("TRXUSDT");
                Console.WriteLine("Закончил работу");
            });
            t.Start();


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
                Console.WriteLine("2");
            });
           
        }
    }
}