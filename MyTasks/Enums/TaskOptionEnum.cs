using System.ComponentModel.DataAnnotations;

namespace MyTasks.Enums
{
    public enum TaskOptionEnum
    {
        All = 0,
        [Display(Name = "Not Started")]
        NotStarted = 1,
        [Display(Name = "In Progress")]
        InProgress = 2,
        Completed = 3,
        Overdue = 4
    }
}
