using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace MyTasksClassLib.Util.CustomValidations
{
    public class AllowExtension : ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowExtension(string[] extensions)
        {
            _extensions = extensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);

                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(
                        ErrorMessage ?? "Invalid File Extension!");
                }
            }
            return ValidationResult.Success;
        }
    }
}
