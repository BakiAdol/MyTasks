using MyTasks.Models;

namespace MyTasks.Services
{
    public interface IMyTaskService
    {
        public void AddNewTask(TaskModel task);
        public List<TaskModel> GetAllTasks();
        public void DeleteATask(int taskId);
        public List<TaskModel> GetOverDueTasks();
    }
}
