using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class UserRolesRepository : Repository<IdentityUserRole<string>>, IUserRolesRepository
    {
        public UserRolesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<IdentityUserRole<string>>> GetAllUserRolesAsync()
        {
            return await GetAll().ToListAsync();
        }
    }
}
