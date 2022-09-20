using MyTasksClassLib.Models;

namespace MyTasksClassLib.DataAccess.Repository.IRepository
{
    public interface IMyTaskService
    {
        public Task AddNewTaskAsync(TaskModel task);
        public Task<AllTasksModel> GetAllTasksAsync(AllTasksModel allTasksModel);
        public Task DeleteATaskAsync(int taskId);
        public Task<TaskModel> GetATaskAsync(int taskId);
        public Task<bool> UpdateATaskAsync(TaskModel updatedTask);
        public Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel);
    }
}
