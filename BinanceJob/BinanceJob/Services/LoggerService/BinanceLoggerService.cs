using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinanceJob.Services.LoggerService
{
    public class BinanceLoggerService : ILoggerService
    {
        public void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"LogLevel Error : {message}");
            Console.ForegroundColor = ConsoleColor.White;
            LoggingToFile($"LogLevel Error : {message}");
        }

        public void Fatal(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"LogLevel Fatal : {message}");
            Console.ForegroundColor = ConsoleColor.White;
            LoggingToFile($"LogLevel Fatal : {message}");
        }

        public void Info(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"LogLevel Info : {message}");
            Console.ForegroundColor = ConsoleColor.White;
            LoggingToFile($"LogLevel Info : {message}");
        }

        public void Success(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"LogLevel Success : {message}");
            Console.ForegroundColor = ConsoleColor.White;
            LoggingToFile($"LogLevel Success : {message}");
        }

        public void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"LogLevel Warn : {message}");
            Console.ForegroundColor = ConsoleColor.White;
            LoggingToFile($"LogLevel Warn : {message}");
        }

        private void LoggingToFile(string message)
        {
            try
            {
                using (StreamWriter fs = new StreamWriter("Log.txt",true))
                {
                    fs.WriteLineAsync(message);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
