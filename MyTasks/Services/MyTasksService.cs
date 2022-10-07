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
        private readonly ITaskRepository _taskRepository;
        private readonly IUserService _userService;
        #endregion

        #region Ctor
        public MyTasksService(ITaskRepository taskRepository, IUserService userService)
        {
            _taskRepository = taskRepository;
            _userService = userService;
        }
        #endregion

        #region Methods

        public async Task GetAllTasksAsync(AllTasksModel allTasksModel)
        {
            string userId = _userService.GetUserId();

            GetTasksModel getTasksModel = new() { 
                UserId = userId,
                Option = allTasksModel.TaskOption,
                TaskHighPriority = allTasksModel.TaskHighPriority,
                TaskMediumPriority = allTasksModel.TaskMediumPriority,
                TaskLowPriority = allTasksModel.TaskLowPriority,
                TaskShowOrder = allTasksModel.OrderOfItemShow,
                CurrentPage = allTasksModel.CurrentPage,
                NumberOfItemShow = allTasksModel.PageItemShow,
                SkipTasks = (allTasksModel.CurrentPage - 1) * allTasksModel.PageItemShow
            };

            allTasksModel.Tasks = 
                await _taskRepository.GetAllTasksAsync(getTasksModel);

            allTasksModel.TotalPages = getTasksModel.TotalPage;
        }

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

                // await _myTaskRepository.AddNewTaskAsync(newEntityTask);
            }
            catch (Exception)
            {
                throw new Exception("Failed to add task!");
            }
        }

        public async Task<DetailViewModel> GetATaskDetailServiceAsync(int taskId)
        {
            //TaskModel task = await _myTaskRepository.GetATaskAsync(taskId);

            //DetailViewModel taskDetail = new()
            //{
            //    Id = task.Id,
            //    MyTask = task.MyTask,
            //    Description = task.Description,
            //    Priority = task.Priority,
            //    Status = task.Status,
            //    CreatedDate = task.CreatedDate,
            //    UpdatedDate = task.UpdatedDate,
            //    DueDate = task.DueDate
            //};

            //return taskDetail;

            return new DetailViewModel();
        }

        public async Task UpdateTaskServiceAsync(DetailViewModel updatedTask)
        {
            //TaskModel task = await _myTaskRepository.GetATaskAsync(updatedTask.Id);

            //task.MyTask = updatedTask.MyTask;
            //task.Description = updatedTask.Description ?? string.Empty;
            //task.Priority = updatedTask.Priority;
            //task.Status = updatedTask.Status;
            //task.DueDate = updatedTask.NewDueDate;
            //task.UpdatedDate = DateTime.Now;

            //await _myTaskRepository.UpdateATaskAsync(task);
        }
        #endregion
    }
}
