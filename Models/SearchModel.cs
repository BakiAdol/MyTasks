using System.ComponentModel.DataAnnotations;

namespace MyTasks.Models
{
    public class SearchModel
    {
        [Required]
        public string SearchText { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public int Option { get; set; }
        [Required]
        public int Order { get; set; }
    }
}
