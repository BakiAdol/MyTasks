using MyTasksClassLib.Util.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Models.ViewModels
{
    public class DetailViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter task.")]
        [StringLength(60)]
        public string MyTask { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        [Required]
        [PrioritySelectValidation]
        public int Priority { get; set; } = 0;

        [Required]
        [TaskStatusValidation]
        public int Status { get; set; } = 0;

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public DateTime DueDate { get; set; }

        [Required]
        [UpdateTasksDueDateValidation]
        public DateTime NewDueDate { get; set; }
    }
}
