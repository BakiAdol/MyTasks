namespace MyTasks.Models
{
    public class Pager
    {
        public int TotalPage { get; set; }
        public int CurrentPageNumber { get; set; } = 1;
        public int PageItemShow { get; set; } = 8;
        public int OrderOfItemShow { get; set; } = 0;
        public int StartPageNumber { get; } = 1;
        public int EndPageNumber { get; set; }
    }
}
