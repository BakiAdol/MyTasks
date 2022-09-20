namespace MyTasksClassLib.Models
{
    public class SearchTasksModel : PagerModel
    {
        public string SearchText { get; set; } = string.Empty;
        public int TaskStatus { get; set; } = 0;
        public int TaskPriority { get; set; } = 0;
    }
}
