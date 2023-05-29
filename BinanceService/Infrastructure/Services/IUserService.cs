using Domain.Entity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        public Task<IdentityUser> CreateUserAsync(ApplicationUser user);
        public Task<IdentityUser> UpdateUser(ApplicationUser user);
        public Task<IdentityUser> DeleteUser(ApplicationUser user);
        public Task<IdentityUser> GetUserFromUserNameTelegramAsync(string username);
        public Task<IdentityUser> GetUserFromId(int id);
    }
}
