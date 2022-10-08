using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;

namespace MyTasks.Services.IServices
{
    public interface IUserService
    {
        Task GetAllUserAsync(SearchUsersModel searchUsersModel);
        Task UpdateUserRoleAsync(string email);
        String GetUserRoleNameAsync(string userId);
        string GetUserId();
        Task<UserModel> GetUserAsync(string userId);
        Task<EditProfileViewModel> GetEditUserAsync(string userId);
        Task UpdateUserProfileAsync(EditProfileViewModel updatedUser);
    }
}
