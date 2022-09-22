namespace MyTasksClassLib.Models
{
    public class PagerModel<T> where T : class
    {
        public List<T> Tasks { get; set; } = new List<T>();
        public int TotalPages { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;
        public int PageItemShow { get; set; } = 2;
        public int OrderOfItemShow { get; set; } = 0;
        public string ControllerName { get; set; } = string.Empty;
        public string ActionName { get; set; } = string.Empty;
    }
}
