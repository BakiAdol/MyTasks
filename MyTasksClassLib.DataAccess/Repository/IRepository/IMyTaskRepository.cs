using MyTasksClassLib.Models;

namespace MyTasksClassLib.DataAccess.Repository.IRepository
{
    public interface IMyTaskRepository
    {
        public Task AddNewTaskAsync(TaskModel task);
        public Task<AllTasksModel> GetAllTasksAsync(AllTasksModel allTasksModel, string userId);
        public Task DeleteATaskAsync(int taskId);
        public Task<TaskModel> GetATaskAsync(int taskId);
        public Task<bool> UpdateATaskAsync(TaskModel updatedTask);
        public Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel, string userId);
        public Task GetAllUser(SearchUsersModel searchUsersModel);
    }
}
