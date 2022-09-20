using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyTasksClassLib.DataAccess;
using MyTasksClassLib.Models;
using System.Threading.Tasks;

namespace MyTasks.Services
{
    public class MyTaskService: IMyTaskService
    {
        #region Props
        private readonly ApplicationDbContext dbContext;
        #endregion

        #region Ctor
        public MyTaskService(ApplicationDbContext dbContext)
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
            List<TaskModel> tasks = null;
            allTasksModel.Tasks = new List<TaskModel>();

            int? taskOption = allTasksModel.TaskOption;
            int taskHighPriority = allTasksModel.TaskHighPriority;
            int taskMediumPriority = allTasksModel.TaskMediumPriority;
            int taskLowPrioriry = allTasksModel.TaskLowPriority;
            int taskShowOrder = allTasksModel.OrderOfItemShow;
            int taskCurrentPage = allTasksModel.CurrentPage;
            int taskNumberOfItemShow = allTasksModel.PageItemShow;

            // all task filter by option
            tasks = await dbContext.MyTasks
                .Where(task => taskOption == null ? task.Status != -1 :
                taskOption == 3 ? task.DueDate < DateTime.Now && task.Status != 2 :
                task.Status == taskOption)
                .ToListAsync();

            if (tasks == null) return allTasksModel;

            bool isUncheckAllPriority = (taskHighPriority == taskMediumPriority
                && taskMediumPriority == taskLowPrioriry && taskLowPrioriry == 0);
            
            if(!isUncheckAllPriority)
            {
                tasks = tasks.Where(task => (
                task.Priority == 0 && taskHighPriority == 1) ||
                (task.Priority == 1 && taskMediumPriority == 1) ||
                (task.Priority == 2 && taskLowPrioriry == 1)).ToList();
            }

            if (taskShowOrder == 0) // latest taks
            {
                tasks = tasks.OrderByDescending(task => task.Id).ToList();
            }
            else // oldest taks
            {
                tasks = tasks.OrderBy(task => task.Id).ToList();
            }

            if(tasks == null) return allTasksModel;

            int skipePages = (taskCurrentPage - 1) * taskNumberOfItemShow;

            allTasksModel.TotalPages = (tasks.Count + taskNumberOfItemShow-1)/taskNumberOfItemShow;
            tasks = tasks.Skip(skipePages)
                .Take(taskNumberOfItemShow)
                .ToList();

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
            List<TaskModel> tasks = null;
            searchTasksModel.Tasks = new List<TaskModel>();

            int srPriority = searchTasksModel.TaskPriority;
            int srOrder = searchTasksModel.OrderOfItemShow;
            int srOption = searchTasksModel.TaskStatus;
            string srText = searchTasksModel.SearchText;
            int srCurrentPage = searchTasksModel.CurrentPage;
            int srPgaeItemShow = searchTasksModel.PageItemShow;

            tasks = await dbContext.MyTasks
                .Where(task => srOption == 0 ? task.Status != -1 :
                    srOption == 4 ? (task.DueDate.Day < DateTime.Now.Day) &&
                    task.Status != 2 : task.Status == srOption - 1)
                .ToListAsync();

            if (tasks == null) return searchTasksModel;

            tasks = tasks
                .Where(task => srPriority == 0 ? task.Priority != -1 : task.Priority == srPriority - 1)
                .ToList();

            if (tasks == null) return searchTasksModel;

            tasks = tasks
                .Where(task => task.MyTask.Contains(srText) ||
                        task.Description.Contains(srText))
                .ToList();

            if (tasks == null) return searchTasksModel;

            tasks = (srOrder == 0 ? tasks.OrderByDescending(task => task.Id) :
                 tasks.OrderBy(task => task.Id)).ToList();

            // pager...........
            if (tasks == null) return searchTasksModel;

            int skipePages = (srCurrentPage - 1) * srPgaeItemShow;

            searchTasksModel.TotalPages = (tasks.Count + srPgaeItemShow - 1) / srPgaeItemShow;
            
            tasks = tasks.Skip(skipePages)
            .Take(srPgaeItemShow)
            .ToList();

            searchTasksModel.Tasks = tasks;

            return searchTasksModel;
        }
        #endregion
    }
}
