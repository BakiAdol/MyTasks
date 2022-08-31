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

        #region
        public void AddNewTask(TaskModel task)
        {
            dbContext.MyTasks.Add(task);
            dbContext.SaveChanges();
        }
        public List<TaskModel> GetAllTasks()
        {
            var tasks = dbContext.MyTasks
                .OrderBy(task => task.Priority)
                .ThenByDescending(task => task.Id)
                .ToList();

            return tasks ?? new List<TaskModel>();
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
            var tasks = dbContext.MyTasks.Where(task => task.DueDate < DateTime.Now)
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
        #endregion
    }
}
