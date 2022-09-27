using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Util.CustomValidations
{
    public class UpdateTasksDueDateValidation : ValidationAttribute
    {
        public DateTime OldDueDate { get; set; }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime inputDate = (DateTime)value;
                DateTime todayDate = DateTime.Now;
                int dueDateComparison = DateTime.Compare(OldDueDate.Date, inputDate.Date);
                if (dueDateComparison != 0)
                {
                    int todaysComparison = DateTime.Compare(todayDate.Date, inputDate.Date);
                    if(todaysComparison == -1)
                    {
                        return new ValidationResult(
                        ErrorMessage ?? "Invalid Due Date!");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}
