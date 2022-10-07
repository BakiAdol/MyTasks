using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;

namespace MyTasks.Services.IServices
{
    public interface IMyTasksService
    {
        Task GetAllTasksAsync(AllTasksModel allTasksModel);
        Task AddNewTaskAsync(AddNewViewModel newTask);
        Task<DetailViewModel> GetATaskDetailAsync(int taskId);
        Task UpdateTaskAsync(DetailViewModel updatedTask);
        Task DeleteTaskAsync(int taskId);
    }
}
