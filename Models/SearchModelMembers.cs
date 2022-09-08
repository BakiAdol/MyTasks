namespace MyTasks.Models
{
    public class SearchModelMembers
    {
        public SearchModel SearchModel { get; set; }
        public IEnumerable<TaskModel> TaskModel { get; set; }
    }
}
