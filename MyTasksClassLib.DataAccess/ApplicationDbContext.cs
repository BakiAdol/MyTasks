using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyTasksClassLib.Models;

namespace MyTasksClassLib.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        #region Ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        #endregion

        #region Props
        public DbSet<TaskModel> MyTasks { get; set; }
        #endregion
    }
}
