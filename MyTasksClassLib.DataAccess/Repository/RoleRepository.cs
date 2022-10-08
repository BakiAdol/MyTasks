using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyTasksClassLib.DataAccess.Repository.IRepository;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class RoleRepository : Repository<IdentityRole>, IRoleRepository
    {
        #region Ctor
        public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
        #endregion

        #region Methods
        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            return await GetAll().ToListAsync();
        }
        #endregion
    }
}
