using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyTasks.Data;
using MyTasks.Models;
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
        public async Task<List<TaskModel>> GetAllTasksAsync(Pager pager, int? option)
        {
            List<TaskModel> tasks = null;

            // all task filter by option
            tasks = await dbContext.MyTasks
                .Where(task => option == null ? task.Status != -1 :
                option == 3 ? task.DueDate < DateTime.Now && task.Status != 2 :
                task.Status == option)
                .ToListAsync();

            if (tasks == null) return new List<TaskModel>();

            bool isUncheckAllPriority = (pager.HighPriority == pager.MediumPriority
                && pager.MediumPriority == pager.LowPriority && pager.LowPriority == 0);
            
            if(!isUncheckAllPriority)
            {
                tasks = tasks.Where(task => (
                task.Priority == 0 && pager.HighPriority == 1) ||
                (task.Priority == 1 && pager.MediumPriority == 1) ||
                (task.Priority == 2 && pager.LowPriority == 1)).ToList();
            }

            if (pager.OrderOfItemShow == 0) // latest taks
            {
                tasks = tasks.OrderByDescending(task => task.Id).ToList();
            }
            else // oldest taks
            {
                tasks = tasks.OrderBy(task => task.Id).ToList();
            }

            if(tasks == null) return new List<TaskModel>();

            int skipePages = (pager.CurrentPageNumber - 1) * pager.PageItemShow;

            pager.TotalPage = (tasks.Count + pager.PageItemShow-1)/pager.PageItemShow;
            tasks = tasks.Skip(skipePages)
                .Take(pager.PageItemShow)
                .ToList();

            return tasks;
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
        public async Task<List<TaskModel>> GetSearchTasksAsync(SearchModel searchInfo, Pager pager)
        {
            List<TaskModel> tasks = null;

            int srPriority = searchInfo.Priority;
            int srOrder = searchInfo.Order;
            int srOption = searchInfo.Option;

            tasks = await dbContext.MyTasks
                .Where(task => srOption == 0 ? task.Status != -1 :
                    srOption == 4 ? (task.DueDate.Day < DateTime.Now.Day) &&
                    task.Status != 2 : task.Status == srOption - 1)
                .ToListAsync();

            if (tasks == null) return new List<TaskModel>();

            tasks = tasks
                .Where(task => srPriority == 0 ? task.Priority != -1 : task.Priority == srPriority - 1)
                .ToList();

            if (tasks == null) return new List<TaskModel>();

            tasks = tasks
                .Where(task => task.MyTask.Contains(searchInfo.SearchText) ||
                        task.Description.Contains(searchInfo.SearchText))
                .ToList();

            if (tasks == null) return new List<TaskModel>();

            tasks = (srOrder == 0 ? tasks.OrderByDescending(task => task.Id) :
                 tasks.OrderBy(task => task.Id)).ToList();

            // pager...........
            if (tasks == null) return new List<TaskModel>();
            int skipePages = (pager.CurrentPageNumber - 1) * pager.PageItemShow;
            pager.TotalPage = (tasks.Count + pager.PageItemShow - 1) / pager.PageItemShow;
            tasks = tasks.Skip(skipePages)
            .Take(pager.PageItemShow)
            .ToList();

            return tasks ?? new List<TaskModel>();
        }
        #endregion
    }
}
