using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;

namespace MyTasks.Services.IServices
{
    public interface IUserService
    {
        string GetUserId();
        Task<UserModel> GetUserAsync(string userId);
        Task<EditProfileViewModel> GetEditUserAsync(string userId);
    }
}
