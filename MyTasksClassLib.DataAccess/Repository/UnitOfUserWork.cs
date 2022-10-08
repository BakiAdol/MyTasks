using Microsoft.AspNetCore.Identity;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class UnitOfUserWork : IUnitOfUserWork
    {
        #region Props
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<UserModel> _userManager;
        #endregion

        #region Ctor
        public UnitOfUserWork(ApplicationDbContext dbContext, UserManager<UserModel> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager; 
            Users = new UserRepository(_dbContext, _userManager);
            Roles = new RoleRepository(_dbContext);
            UserRoles = new UserRolesRepository(_dbContext);
        }
        #endregion

        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IUserRolesRepository UserRoles { get; private set; }
    }
}
