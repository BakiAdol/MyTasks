using MyTasksClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUserAsync(GetUserModel usersInfo);
        Task<UserModel> GetUserAsync(string userId);
        String GetUserRoleName(string userId);
        Task UpdateUserAsync(UserModel user);
        Task UpdateUserRole(string Email);
    }
}
