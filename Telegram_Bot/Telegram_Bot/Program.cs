using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram_Bot.services;
using Telegram_Bot.Database;
using Microsoft.EntityFrameworkCore;

namespace Telegram_Bot
{
    public class Program
    {
        public static void Main(string[] args)
        {
           Initialization();
           Console.ReadKey();
        }

        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;
            if (message?.Text is not null)
            {
                Console.WriteLine($"Сообщение написал: {message.Chat.LastName} {message.Chat.FirstName} - {message.Text}");
                if (!UserService.UserInfo(message))
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Привет, с пополнением в наши ряды");
                }
                else
                {
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Привет, я отработал твое сообщение!");
                }
                
            }
            
        }        

        public static void Initialization()
        {
            /*
            *Добавление миграции
            */
            using (TelegramContext db = new TelegramContext())
            {
                db.Database.Migrate();
            }

            var botClient = new TelegramBotClient("5956542091:AAHrExySOH-Q17MJBFbvD8OMoCpj_3hyH7U");

            botClient.StartReceiving(Update, Error);

            Console.WriteLine("Начал обработку сообщений");
        }
    }
}