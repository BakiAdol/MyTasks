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
        public List<TaskModel> GetAllTasks(Pager pager)
        {
            List<TaskModel> tasks = null;

            if(pager.OrderOfItemShow == 0)
            {
                tasks = dbContext.MyTasks
                    .OrderByDescending(task => task.Id)
                    .ThenBy(task => task.Priority)
                    .ToList();
            }
            else if(pager.OrderOfItemShow == 1)
            {
                tasks = dbContext.MyTasks
                    .OrderBy(task => task.Priority)
                    .ThenByDescending(task => task.Id)
                    .ToList();
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
        public List<TaskModel> GetOverDueTasks()
        {
            var tasks = dbContext.MyTasks
                .Where(task => task.DueDate < DateTime.Now && task.Status != 2)
                .OrderBy(task => task.Priority)
                .ThenByDescending(task => task.Id)
                .ToList();

            return tasks ?? new List<TaskModel>();
        }
        public List<TaskModel> GetStatusTasks(int needStatus)
        {
            var tasks = dbContext.MyTasks
                        .Where(item => item.Status == needStatus)
                        .OrderBy(task => task.Priority)
                        .ThenByDescending(task => task.Id)
                        .ToList();

            return tasks ?? new List<TaskModel>();
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

            dbContext.SaveChanges();
        }
        #endregion
    }
}
