namespace MyTasks.Models
{
    public class PagerModel
    {
        public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
        public int TotalPages { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;
        public int PageItemShow { get; set; } = 4;
        public int OrderOfItemShow { get; set; } = 0;
        public string ControllerName { get; set; } = String.Empty;
        public string ActionName { get; set; } = String.Empty;
    }
}
