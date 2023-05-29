using Domain.Entity.Models;
using Infrastructure.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramService.Services
{
    public class TelegramBotService
    {
        ILogger _logger;
        IUserService _userService;

        private ITelegramBotClient _botClient = new TelegramBotClient("5956542091:AAHrExySOH-Q17MJBFbvD8OMoCpj_3hyH7U");
        public TelegramBotService(ILogger logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
            //StartTelegramBot();
        }

        public void StartTelegramBot()
        {
            _logger.Information("Телеграм начал обработку сообщений");

            _botClient.StartReceiving(Update, Error);
        }

        private Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            _logger.Error(arg2.Message);
            throw new NotImplementedException();
        }

        private async Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            var message = update.Message;

            var findUser = await _userService.GetUserFromUserNameTelegramAsync(message.From.Username);
            if (findUser is null)
            {
                ApplicationUser newUser = new ApplicationUser()
                {
                    TelegramInfo = new()
                    {
                        UserName = message.From.Username,
                        ChatId = message.From.Id,
                        LastName = message.From.LastName,
                        Name = message.From.FirstName
                    },
                };

                await _userService.CreateUserAsync(newUser);

                await _botClient.SendTextMessageAsync(message.Chat.Id, $"Поздравляем, {newUser.TelegramInfo.UserName}, теперь ты в нашей тусовке");
            }
            else
            {
                await _botClient.SendTextMessageAsync(message.Chat.Id, $"Вы зарегистрированы.\nЭтот бот не умеет выполнять команды ");
            }
        }

        public async Task SendMessageTelegramBotAsync(long chatId, string message)
        {
            await _botClient.SendTextMessageAsync(chatId, message);
        }
    }
}
