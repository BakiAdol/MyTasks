//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyTasksClassLib.DataAccess;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.DataAccess.TasksFilter;
using MyTasksClassLib.Models;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class MyTaskRepository: IMyTaskRepository
    {
        #region Props
        private readonly ApplicationDbContext dbContext;
        #endregion

        #region Ctor
        public MyTaskRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        #endregion

        #region Methods
        public async Task AddNewTaskAsync(TaskModel task)
        {
            dbContext.MyTasks.Add(task);
            await dbContext.SaveChangesAsync();
        }
        public async Task<AllTasksModel> GetAllTasksAsync(AllTasksModel allTasksModel)
        {
            List<TaskModel> tasks;
            allTasksModel.Tasks = new List<TaskModel>();

            int? taskOption = allTasksModel.TaskOption;
            int taskHighPriority = allTasksModel.TaskHighPriority;
            int taskMediumPriority = allTasksModel.TaskMediumPriority;
            int taskLowPrioriry = allTasksModel.TaskLowPriority;
            int taskShowOrder = allTasksModel.OrderOfItemShow;
            int taskCurrentPage = allTasksModel.CurrentPage;
            int taskNumberOfItemShow = allTasksModel.PageItemShow;

            int skipTasks = (taskCurrentPage - 1) * taskNumberOfItemShow;

            tasks = await dbContext.MyTasks
                    .GetOptionTasks(taskOption)
                    .GetPriorityFilterTasks(taskHighPriority, taskMediumPriority, taskLowPrioriry)
                    .OrderTasks(taskShowOrder)
                    .ToListAsync();

            allTasksModel.TotalPages = 
                (tasks.Count + taskNumberOfItemShow - 1) / taskNumberOfItemShow;
            
            tasks = tasks.Skip(skipTasks).Take(taskNumberOfItemShow).ToList();

            tasks ??= new List<TaskModel>();

            allTasksModel.Tasks = tasks;

            return allTasksModel;
        }
        public async Task DeleteATaskAsync(int taskId)
        {
            var task = await dbContext.MyTasks.FirstOrDefaultAsync(item => item.Id == taskId);
            if(task != null)
            {
                dbContext.MyTasks.Remove(task);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<TaskModel> GetATaskAsync(int taskId)
        {
            var task = await dbContext.MyTasks.FirstOrDefaultAsync(item => item.Id == taskId);
            return task ?? new TaskModel();
        }
        public async Task<bool> UpdateATaskAsync(TaskModel updatedTask)
        {
            var oldTask = await dbContext.MyTasks.FirstOrDefaultAsync(item => item.Id == updatedTask.Id);

            if (oldTask == null) return false;
            if (updatedTask.MyTask == null) return false;
            if (updatedTask.DueDate != oldTask.DueDate
                && updatedTask.DueDate < DateTime.Now) {

                updatedTask.DueDate = oldTask.DueDate;

                return false;
            }

            oldTask.MyTask = updatedTask.MyTask;
            oldTask.Status = updatedTask.Status;
            oldTask.Priority = updatedTask.Priority;
            oldTask.DueDate = updatedTask.DueDate;
            oldTask.UpdatedDate = DateTime.Now;
            oldTask.Description = updatedTask.Description??"";

            await dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel)
        {
            List<TaskModel> tasks;
            searchTasksModel.Tasks = new List<TaskModel>();

            int srPriority = searchTasksModel.TaskPriority;
            int srOrder = searchTasksModel.OrderOfItemShow;
            int srOption = searchTasksModel.TaskStatus;
            string srText = searchTasksModel.SearchText;
            int srCurrentPage = searchTasksModel.CurrentPage;
            int srPgaeItemShow = searchTasksModel.PageItemShow;

            int skipeTasks = (srCurrentPage - 1) * srPgaeItemShow;

            tasks = await dbContext.MyTasks
                    .GetSearchOptionTasks(srOption)
                    .GetSearchPriorityFilterTasks(srPriority)
                    .GetPatternMatchingTasks(srText)
                    .OrderTasks(srOrder)
                    .ToListAsync();

            searchTasksModel.TotalPages = (tasks.Count + srPgaeItemShow - 1) / srPgaeItemShow;

            tasks = tasks.Skip(skipeTasks).Take(srPgaeItemShow).ToList();

            tasks ??= new List<TaskModel>();

            searchTasksModel.Tasks = tasks;

            return searchTasksModel;
        }
        #endregion
    }
}
