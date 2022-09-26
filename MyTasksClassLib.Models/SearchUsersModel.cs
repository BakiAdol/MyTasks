using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.Models
{
    public class SearchUsersModel : PagerModel<UserModel>
    {
        public string SearchText { get; set; }
    }
}
