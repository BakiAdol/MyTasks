using Microsoft.AspNetCore.Http;
using MyTasksClassLib.Util.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Models.ViewModels
{
    public class EditProfileViewModel
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Upload)]
        [AllowExtension(new string[] { ".jpg", ".png" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile? ProfilePicture { get; set; }
    }
}
