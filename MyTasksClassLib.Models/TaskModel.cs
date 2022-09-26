using MyTasksClassLib.Util.CustomValidations;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MyTasksClassLib.Models
{
    public class TaskModel
    {
        [Key]
        public int Id { get; set; } = 0;

        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter task.")]
        [StringLength(60)]
        public string MyTask { get; set; }
        public string? Description { get; set; } = string.Empty;

        [Required]
        [PrioritySelectValidation]
        public int Priority { get; set; } = 0;
        public int Status { get; set; } = 0;

        [Required]
        [AddNewTaskDueDateValidation]
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedSource { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedSource { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
