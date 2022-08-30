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
        #endregion
    }
}
