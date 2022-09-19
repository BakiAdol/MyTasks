namespace MyTasks.Models
{
    public class SearchTasksModel : PagerModel
    {
        public string SearchText { get; set; } = String.Empty;
        public int TaskStatus { get; set; } = 0;
        public int TaskPriority { get; set; } = 0;
    }
}
