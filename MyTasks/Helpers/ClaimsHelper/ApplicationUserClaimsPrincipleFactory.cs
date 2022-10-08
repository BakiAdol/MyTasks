using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyTasks.Services.IServices;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;
using System.Security.Claims;

namespace MyTasks.Helpers.ClaimsHelper
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserModel, IdentityRole>
    {
        #region Props
        private readonly IUserService _userService;
        #endregion

        #region Ctor
        public ApplicationUserClaimsPrincipalFactory(UserManager<UserModel> userManager, 
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options, IUserService userService)
            : base(userManager, roleManager, options)
        {
            _userService = userService;
        }
        #endregion

        #region Methods
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserModel user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Name", user.Name ?? ""));
            var loggedInUserRoleName = _userService.GetUserRoleNameAsync(user.Id);
            identity.AddClaim(new Claim("RoleName", loggedInUserRoleName));

            return identity;
        }
        #endregion
    }
}
