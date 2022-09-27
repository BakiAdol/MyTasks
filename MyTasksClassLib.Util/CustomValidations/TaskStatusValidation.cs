using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Util.CustomValidations
{
    public class TaskStatusValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || (int)value < 0 || (int)value > 2)
            {
                return new ValidationResult(
                        ErrorMessage ?? "Invalid Task Status!");
            }
            return ValidationResult.Success;
        }
    }
}
