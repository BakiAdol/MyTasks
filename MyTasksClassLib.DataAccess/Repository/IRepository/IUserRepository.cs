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
    }
}
