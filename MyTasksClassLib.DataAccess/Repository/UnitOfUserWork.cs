using MyTasksClassLib.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class UnitOfUserWork : IUnitOfUserWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfUserWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Users = new UserRepository(_dbContext);
            Roles = new RoleRepository(_dbContext);
            UserRoles = new UserRolesRepository(_dbContext);
        }

        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IUserRolesRepository UserRoles { get; private set; }
    }
}
