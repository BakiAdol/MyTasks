using Microsoft.EntityFrameworkCore;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.DataAccess.TasksFilter;
using MyTasksClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class TaskRepository : Repository<TaskModel>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        {
        }

        public Task AddNewTaskAsync(TaskModel newTask)
        {
            throw new NotImplementedException();
        }

        public Task DeleteATaskAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TaskModel>> GetAllTasksAsync(GetTasksModel tasksInfo)
        {
            List<TaskModel> tasks = await GetAll()
                        .Where(task => task.UserId == tasksInfo.UserId)
                        .GetOptionTasks(tasksInfo.Option)
                        .GetPriorityFilterTasks(tasksInfo.TaskHighPriority,
                            tasksInfo.TaskMediumPriority, tasksInfo.TaskLowPriority)
                        .OrderTasks(tasksInfo.TaskShowOrder)
                        .ToListAsync();

            tasks ??= new List<TaskModel>();

            tasksInfo.TotalPage = 
                (tasks.Count + tasksInfo.NumberOfItemShow - 1) / tasksInfo.NumberOfItemShow;

            tasks = tasks
                    .Skip(tasksInfo.SkipTasks)
                    .Take(tasksInfo.NumberOfItemShow).ToList();

            return tasks;
        }

        public Task<TaskModel> GetATaskAsync(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateATaskAsync(TaskModel updatedTask)
        {
            throw new NotImplementedException();
        }
    }
}
