using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository.IRepository
{
    public interface IUserRolesRepository
    {
        Task<List<IdentityUserRole<string>>> GetAllUserRolesAsync();
    }
}
