using MyTasks.Models;

namespace MyTasks.Services
{
    public interface IMyTaskService
    {
        public void AddNewTask(TaskModel task);
        public List<TaskModel> GetAllTasks();
        public void DeleteATask(int taskId);
        public List<TaskModel> GetOverDueTasks();
        public List<TaskModel> GetStatusTasks(int needStatus);
        public TaskModel GetATask(int taskId);
        public void UpdateATask(TaskModel updatedTask);
    }
}
