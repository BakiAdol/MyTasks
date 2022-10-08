using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;

namespace MyTasksClassLib.DataAccess.Repository
{
    class UserRepository : Repository<UserModel>, IUserRepository
    {
        #region Props
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<UserModel> _userManager;
        #endregion

        #region Ctor
        public UserRepository(ApplicationDbContext dbContext, 
            UserManager<UserModel> userManager) : base(dbContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }
        #endregion

        #region Methods
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

        public async Task<UserModel> GetUserAsync(string userId)
        {
            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (dbUser == null) return new UserModel();

            var dbUserRole = await _dbContext.UserRoles.FirstOrDefaultAsync(u => u.UserId == dbUser.Id);
            if (dbUserRole == null) return new UserModel();

            var userRoleName = await _dbContext.Roles.Where(r => r.Id == dbUserRole.RoleId)
                .Select(e => e.Name).FirstOrDefaultAsync();

            dbUser.RoleName = userRoleName ?? string.Empty;
            return dbUser;
        }

        public String GetUserRoleName(string userId)
        {
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Id == userId);
            if (dbUser == null) return "User Role Not Found!";
            var dbUserRole = _dbContext.UserRoles.FirstOrDefault(u => u.UserId == dbUser.Id);
            if (dbUserRole == null) return "User Role Not Found!";
            var userRoleName = _dbContext.Roles.Where(r => r.Id == dbUserRole.RoleId)
                .Select(e => e.Name).FirstOrDefault();

            return userRoleName ?? string.Empty;
        }

        public async Task UpdateUserAsync(UserModel user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdateUserRole(string Email)
        {
            var dbUser = _dbContext.Users.FirstOrDefault(u => u.Email == Email);
            if (dbUser == null) return;

            var dbUserRole = _dbContext.UserRoles.FirstOrDefault(u => u.UserId == dbUser.Id);

            var prevRoleName = "User";

            if (dbUserRole != null)
            {
                prevRoleName = _dbContext.Roles.Where(r => r.Id == dbUserRole.RoleId)
                .Select(e => e.Name).FirstOrDefault();
            }

            await _userManager.RemoveFromRoleAsync(dbUser, prevRoleName);
            await _userManager.AddToRoleAsync(dbUser,
                prevRoleName == "Admin" ? "User" : "Admin");

        }
        #endregion
    }
}
