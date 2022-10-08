using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.DataAccess.TasksFilter;
using MyTasksClassLib.Models;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class TaskRepository : Repository<TaskModel>, ITaskRepository
    {
        #region Props
        private readonly ApplicationDbContext _dbContext;
        #endregion

        #region Ctor
        public TaskRepository(ApplicationDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task AddNewTaskAsync(TaskModel newTask)
        {
            await CreateAsync(newTask);
        }

        public async Task<TaskModel> GetATaskAsync(int taskId)
        {
            var task = await _dbContext.MyTasks.FirstOrDefaultAsync(item => item.Id == taskId);
            return task ?? new TaskModel();
        }

        public async Task UpdateATaskAsync(TaskModel updatedTask)
        {
            await UpdateAsync(updatedTask);
        }

        public async Task DeleteATaskAsync(TaskModel taskToDelete)
        {
            await DeleteAsync(taskToDelete);
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

        public async Task<List<TaskModel>> GetSearchTasksAsync(GetSearchTasksModel tasksInfo)
        {
            List<TaskModel> tasks = await GetAll()
                      .Where(task => task.UserId == tasksInfo.UserId)
                      .GetSearchOptionTasks(tasksInfo.Option)
                      .GetSearchPriorityFilterTasks(tasksInfo.TaskPriority)
                      .GetPatternMatchingTasks(tasksInfo.SearchText)
                      .OrderTasks(tasksInfo.TaskShowOrder)
                      .ToListAsync();

            tasks ??= new List<TaskModel>();

            tasksInfo.TotalPage =
                (tasks.Count + tasksInfo.NumberOfItemShow - 1) / tasksInfo.NumberOfItemShow;

            tasks = tasks
                    .Skip(tasksInfo.SkipTasks)
                    .Take(tasksInfo.NumberOfItemShow)
                    .ToList();

            return tasks;
        }
        #endregion
    }
}
