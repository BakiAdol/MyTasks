using MyTasksClassLib.Util.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Models.ViewModels
{
    public class AddNewViewModel
    {
        [Required(ErrorMessage = "Please Enter task.")]
        [StringLength(60)]
        public string MyTask { get; set; }

        public string? Description { get; set; } = string.Empty;

        [Required]
        [PrioritySelectValidation]
        public int Priority { get; set; } = 0;

        [Required]
        [AddNewTaskDueDateValidation]
        public DateTime DueDate { get; set; }
    }
}
