using MyTasks.Services.IServices;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;
using System.Security.Claims;

namespace MyTasks.Services
{
    public class UserService : IUserService
    {
        #region Prop
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMyTaskRepository _myTaskRepository;
        #endregion

        #region Ctor
        public UserService(IHttpContextAccessor contextAccessor, 
            IMyTaskRepository myTaskRepository)
        {
            _contextAccessor = contextAccessor;
            _myTaskRepository = myTaskRepository;
        }
        #endregion

        #region Methods
        public string GetUserId()
        {
            return _contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public async Task<EditProfileViewModel> GetEditUserAsync(string userId)
        {
            var userDetail = await _myTaskRepository.GetUserAsync(userId);

            EditProfileViewModel user = new()
            {
                Name = userDetail.Name,
                Email = userDetail.Email
            };

            return user;
        }
        public async Task<UserModel> GetUserAsync(string userId)
        {
            var userDetail = await _myTaskRepository.GetUserAsync(userId);
            return userDetail;
        }
        #endregion
    }
}
