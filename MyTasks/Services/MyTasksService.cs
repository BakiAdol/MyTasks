using Microsoft.EntityFrameworkCore;
using MyTasks.Services.IServices;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;

namespace MyTasks.Services
{
    public class MyTasksService : IMyTasksService
    {
        #region Props
        private readonly IMyTaskRepository _myTaskRepository;
        #endregion

        #region Ctor
        public MyTasksService(IMyTaskRepository myTaskRepository)
        {
            _myTaskRepository = myTaskRepository;
        }
        #endregion

        #region Methods
        public async Task AddNewTaskServiceAsync(AddNewViewModel newTask, string userId)
        {
            try
            {
                TaskModel newEntityTask = new()
                {
                    MyTask = newTask.MyTask,
                    Description = newTask.Description ?? string.Empty,
                    Priority = newTask.Priority,
                    DueDate = newTask.DueDate,
                    UserId = userId
                };

                await _myTaskRepository.AddNewTaskAsync(newEntityTask);
            }
            catch (Exception)
            {
                throw new Exception("Failed to add task!");
            }
        }
        #endregion
    }
}
