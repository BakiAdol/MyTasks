using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;
using System.Security.Claims;

namespace MyTasks.Helpers.ClaimsHelper
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserModel, IdentityRole>
    {
        private readonly IMyTaskRepository _myTaskRepository;

        public ApplicationUserClaimsPrincipalFactory(UserManager<UserModel> userManager, 
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options, IMyTaskRepository myTaskRepository)
            : base(userManager, roleManager, options)
        {
            _myTaskRepository = myTaskRepository;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserModel user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Name", user.Name ?? ""));
            var loggedInUserRoleName = _myTaskRepository.GetUserRoleName(user.Id);
            identity.AddClaim(new Claim("RoleName", loggedInUserRoleName));

            return identity;
        }
    }
}
