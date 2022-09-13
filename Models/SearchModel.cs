using System.ComponentModel.DataAnnotations;

namespace MyTasks.Models
{
    public class SearchModel
    {
        [Required]
        public string SearchText { get; set; } = string.Empty;
        [Required]
        public int Priority { get; set; } = 0;
        [Required]
        public int Option { get; set; } = 0;
        [Required]
        public int Order { get; set; } = 0;
    }
}
