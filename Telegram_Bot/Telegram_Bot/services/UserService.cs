using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram_Bot.Database;
using Telegram_Bot.models;

namespace Telegram_Bot.services
{
    public class UserService
    {
        /*
         *Вернет true, если пользователь есть.
         *Вернет false, если пользователя нет         
         */
        public static bool UserInfo(Message? message)
        {
            using(TelegramContext contextDb = new TelegramContext())
            {
                if(contextDb.Users.FirstOrDefault(elem => elem.ChatId == message.Chat.Id) is null ? false : true)
                {
                    return true;
                }
                else
                {
                    CreateNewUser(new models.User(message.Chat.Id, message.Chat.FirstName, message.Chat.LastName, message.Chat.Username));
                    return false;
                }
            }
        }

        private static void CreateNewUser(Telegram_Bot.models.User user)
        {
            using (TelegramContext contextDb = new TelegramContext())
            {
                contextDb.Users.Add(user);
                contextDb.SaveChanges();
            }
        }
    }
}
