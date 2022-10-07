using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;

namespace MyTasks.Services.IServices
{
    public interface IUserService
    {
        Task GetAllUserAsync(SearchUsersModel searchUsersModel);


        string GetUserId();
        Task<UserModel> GetUserAsync(string userId);
        Task<EditProfileViewModel> GetEditUserAsync(string userId);
        Task UpdateUserProfileAsync(EditProfileViewModel updatedUser);
    }
}
