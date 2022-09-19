using MyTasks.Models;

namespace MyTasks.Services
{
    public interface IMyTaskService
    {
        public Task AddNewTaskAsync(TaskModel task);
        public Task<List<TaskModel>> GetAllTasksAsync(Pager pager, int? option);
        public Task DeleteATaskAsync(int taskId);
        public Task<TaskModel> GetATaskAsync(int taskId);
        public Task<bool> UpdateATaskAsync(TaskModel updatedTask);
        public Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel);
    }
}
