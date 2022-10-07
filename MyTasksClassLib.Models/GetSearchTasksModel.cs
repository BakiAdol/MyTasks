using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Models
{
    public class GetSearchTasksModel
    {
        public string UserId { get; set; } = string.Empty;
        public int Option { get; set; }
        public int TaskPriority { get; set; }
        public int TaskShowOrder { get; set; }
        public string SearchText { get; set; } = string.Empty;
        public int CurrentPage { get; set; }
        public int NumberOfItemShow { get; set; }
        public int SkipTasks { get; set; }
        public int TotalPage { get; set; }
    }
}
