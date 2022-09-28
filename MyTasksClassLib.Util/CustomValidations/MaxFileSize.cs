using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Util.CustomValidations
{
    public class MaxFileSize : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSize(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if(file.Length > _maxFileSize)
                {
                    return new ValidationResult(
                        ErrorMessage ?? 
                        $"File size must be less than {_maxFileSize/1024} MB!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
