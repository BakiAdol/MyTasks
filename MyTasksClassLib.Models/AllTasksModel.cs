namespace MyTasksClassLib.Models
{
    public class AllTasksModel : PagerModel<TaskModel>
    {
        public int TaskHighPriority { get; set; } = 0;
        public int TaskMediumPriority { get; set; } = 0;
        public int TaskLowPriority { get; set; } = 0;
        public int? TaskOption { get; set; }
    }
}
