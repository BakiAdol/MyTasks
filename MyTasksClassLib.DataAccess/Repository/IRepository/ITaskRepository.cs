using MyTasksClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository.IRepository
{
    public interface ITaskRepository
    {
        Task AddNewTaskAsync(TaskModel newTask);
        Task<List<TaskModel>> GetAllTasksAsync(GetTasksModel tasksInfo);
        Task DeleteATaskAsync(int taskId);
        Task<TaskModel> GetATaskAsync(int taskId);
        Task UpdateATaskAsync(TaskModel updatedTask);
        Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel, string userId);
    }
}
