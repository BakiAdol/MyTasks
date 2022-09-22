using MyTasks.Services.IServices;
using System.Security.Claims;

namespace MyTasks.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public UserService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId()
        {
            return _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
