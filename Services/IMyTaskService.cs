using MyTasks.Models;

namespace MyTasks.Services
{
    public interface IMyTaskService
    {
        public void AddNewTask(TaskModel task);
        public List<TaskModel> GetAllTasks(Pager pager, int? option);
        public void DeleteATask(int taskId);
        public TaskModel GetATask(int taskId);
        public void UpdateATask(TaskModel updatedTask);
    }
}
