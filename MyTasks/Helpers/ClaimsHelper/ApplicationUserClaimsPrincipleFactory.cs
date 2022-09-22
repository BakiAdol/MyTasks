using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MyTasksClassLib.Models;
using System.Security.Claims;

namespace MyTasks.Helpers.ClaimsHelper
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserModel, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<UserModel> userManager, 
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserModel user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("Name", user.Name ?? ""));

            return identity;
        }
    }
}
