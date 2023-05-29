using DAL.Context;
using Domain.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        ILogger _logger;
        ApplicationContext _context;

        public UserService(ILogger logger, ApplicationContext context)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<IdentityUser> CreateUserAsync(ApplicationUser user)
        {
            if (await _context.Users.Include(e => e.TelegramInfo).Where(u =>            
                (u.TelegramInfo.ChatId == user.TelegramInfo.ChatId ||
                u.TelegramInfo.UserName == user.TelegramInfo.UserName)
            ).FirstOrDefaultAsync() == null)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                _logger.Information($"Пользователь с username: {user.TelegramInfo.UserName} успешно создан");
                return user;
            }

            return null;
        }

        #region todo

        public Task<IdentityUser> DeleteUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> GetUserFromId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IdentityUser> GetUserFromUserNameTelegramAsync(string username)
        => await _context.Users.Include(e => e.TelegramInfo).Where(u => u.TelegramInfo.UserName == username).AsNoTracking().FirstOrDefaultAsync();
        

        public Task<IdentityUser> UpdateUser(ApplicationUser user)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
