using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;

namespace MyTasksClassLib.DataAccess.Repository.IRepository
{
    public interface IMyTaskRepository
    {
        Task AddNewTaskAsync(TaskModel newTask);
        public Task<AllTasksModel> GetAllTasksAsync(AllTasksModel allTasksModel, string userId);
        public Task DeleteATaskAsync(int taskId);
        public Task<TaskModel> GetATaskAsync(int taskId);
        public Task UpdateATaskAsync(TaskModel updatedTask);
        public Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel, string userId);
        public Task GetAllUser(SearchUsersModel searchUsersModel);
        public Task UpdateUserRole(string Email);
    }
}
