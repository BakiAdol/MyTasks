using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Models
{
    public class GetUserModel
    {
        public string SearchText { get; set; } = string.Empty;
        public int SkipUsers { get; set; }
        public int UsersShow { get; set; }
        public int TotalPages { get; set; }
    }
}
