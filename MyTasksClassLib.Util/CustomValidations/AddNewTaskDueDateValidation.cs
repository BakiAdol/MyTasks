using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Util.CustomValidations
{
    public class AddNewTaskDueDateValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime inputDate = (DateTime)value;
                if (DateTime.Compare(DateTime.Now.Date, inputDate.Date) > 0)
                {
                    return new ValidationResult(
                        ErrorMessage ?? "Invalid Due date!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
