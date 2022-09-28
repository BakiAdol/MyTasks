using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;

namespace MyTasksClassLib.DataAccess.Repository.IRepository
{
    public interface IMyTaskRepository
    {
        Task AddNewTaskAsync(TaskModel newTask);
        Task<AllTasksModel> GetAllTasksAsync(AllTasksModel allTasksModel, string userId);
        Task DeleteATaskAsync(int taskId);
        Task<TaskModel> GetATaskAsync(int taskId);
        Task UpdateATaskAsync(TaskModel updatedTask);
        Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel, string userId);
        Task GetAllUser(SearchUsersModel searchUsersModel);
        Task UpdateUserRole(string Email);
        String GetUserRoleName(string userId);
        Task<UserModel> GetUserAsync(string userId);
    }
}
