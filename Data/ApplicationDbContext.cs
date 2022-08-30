using Microsoft.EntityFrameworkCore;
using MyTasks.Models;

namespace MyTasks.Data
{
    public class ApplicationDbContext: DbContext
    {
        #region Ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { 
        }
        #endregion

        #region Props
        public DbSet<TaskModel> MyTasks { get; set; }
        #endregion
    }
}
