using Microsoft.EntityFrameworkCore;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository
{
    class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<UserModel>> GetAllUserAsync(GetUserModel usersInfo)
        {
            var users = await GetAll().ToListAsync();

            if (usersInfo.SearchText != null)
            {
                users = users
                    .Where(user => user.Name.Contains(usersInfo.SearchText,
                        StringComparison.InvariantCultureIgnoreCase))
                    .ToList();
            }

            users ??= new List<UserModel>();

            usersInfo.TotalPages =
                (users.Count + usersInfo.UsersShow - 1) 
                    /usersInfo.UsersShow;

            users = 
                users.Skip(usersInfo.SkipUsers)
                     .Take(usersInfo.UsersShow)
                     .ToList();

            return users;
        }
    }
}
