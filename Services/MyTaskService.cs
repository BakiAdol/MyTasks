using Microsoft.EntityFrameworkCore;
using MyTasks.Data;
using MyTasks.Models;

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
        public void AddNewTask(TaskModel task)
        {
            dbContext.MyTasks.Add(task);
            dbContext.SaveChanges();
        }
        public List<TaskModel> GetAllTasks(Pager pager, int? option)
        {
            List<TaskModel> tasks = null;

            // all task filter by option
            tasks = dbContext.MyTasks
                .Where(task => option == null ? task.Status != -1 :
                option == 3 ? task.DueDate < DateTime.Now && task.Status != 2 :
                task.Status == option)
                .ToList();

            if (tasks == null) return new List<TaskModel>();

            if (pager.OrderOfItemShow == 0) // only sort date wise
            {
                tasks = tasks.OrderByDescending(task => task.Id).ToList();
            }
            else // sort priority wise then date
            {
                tasks = tasks.OrderBy(task => task.Priority)
                    .ThenByDescending(task => task.Id).ToList();
            }

            if(tasks == null) return new List<TaskModel>();

            int skipePages = (pager.CurrentPageNumber - 1) * pager.PageItemShow;

            pager.TotalPage = (tasks.Count + pager.PageItemShow-1)/pager.PageItemShow;
            tasks = tasks.Skip(skipePages)
                .Take(pager.PageItemShow)
                .ToList();

            return tasks;
        }
        public void DeleteATask(int taskId)
        {
            var task = dbContext.MyTasks.FirstOrDefault(item => item.Id == taskId);
            if(task != null)
            {
                dbContext.MyTasks.Remove(task);
                dbContext.SaveChanges();
            }
        }
        public TaskModel GetATask(int taskId)
        {
            var task = dbContext.MyTasks.FirstOrDefault(item => item.Id == taskId);
            return task ?? new TaskModel();
        }
        public void UpdateATask(TaskModel updatedTask)
        {
            var oldTask = dbContext.MyTasks.FirstOrDefault(item => item.Id == updatedTask.Id);

            if (oldTask == null) return;

            oldTask.MyTask = updatedTask.MyTask;
            oldTask.Status = updatedTask.Status;
            oldTask.Priority = updatedTask.Priority;
            oldTask.DueDate = updatedTask.DueDate;
            oldTask.UpdatedDate = DateTime.Now;
            oldTask.Description = updatedTask.Description;

            dbContext.SaveChanges();
        }
        public List<TaskModel> GetSearchTasks(SearchModel searchInfo)
        {
            List<TaskModel> tasks = null;

            int srPriority = searchInfo.Priority;
            int srOrder = searchInfo.Order;
            int srOption = searchInfo.Option;

            tasks = dbContext.MyTasks
                    .Where(task => (srOption == 0 ? task.Status != -1 :
                    srOption == 4 ? task.DueDate.Day < DateTime.Now.Day && task.Status != 2 :
                    task.Status == srOption-1) &&
                    (srPriority==0 ? task.Priority != -1 :
                    task.Priority == srPriority-1) &&
                        task.MyTask.Contains(searchInfo.SearchText))
                    .ToList();
            
            if(tasks == null) return new List<TaskModel>();

            tasks = (srOrder == 0 ? tasks.OrderByDescending(task => task.Id) :
                 tasks.OrderBy(task => task.Id)).ToList();
            
            if(srOrder == 0)
            tasks = tasks.OrderBy(task => task.Priority).ToList();

            return tasks ?? new List<TaskModel>();
        }
        #endregion
    }
}
