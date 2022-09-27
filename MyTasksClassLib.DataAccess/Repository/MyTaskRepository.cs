//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using MyTasksClassLib.DataAccess;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.DataAccess.TasksFilter;
using MyTasksClassLib.Models;
using MyTasksClassLib.Models.ViewModels;
using System.Threading.Tasks;

namespace MyTasksClassLib.DataAccess.Repository
{
    public class MyTaskRepository: IMyTaskRepository
    {
        #region Props
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<UserModel> _userManager;
        #endregion

        #region Ctor
        public MyTaskRepository(ApplicationDbContext dbContext, UserManager<UserModel> _userManager)
        {
            this.dbContext = dbContext;
            this._userManager = _userManager;
        }
        #endregion

        #region Methods
        public async Task AddNewTaskAsync(AddNewViewModel newTask, string userId)
        {
            try
            {
                TaskModel newEntityTask = new()
                { 
                    MyTask = newTask.MyTask,
                    Description = newTask.Description??string.Empty,
                    Priority = newTask.Priority,
                    DueDate = newTask.DueDate,
                    UserId = userId
                };

                dbContext.MyTasks.Add(newEntityTask);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Failed to add task!");
            }
        }
        public async Task<AllTasksModel> GetAllTasksAsync(AllTasksModel allTasksModel, string userId)
        {
            List<TaskModel> tasks;
            allTasksModel.Tasks = new List<TaskModel>();

            int? taskOption = allTasksModel.TaskOption;
            int taskHighPriority = allTasksModel.TaskHighPriority;
            int taskMediumPriority = allTasksModel.TaskMediumPriority;
            int taskLowPrioriry = allTasksModel.TaskLowPriority;
            int taskShowOrder = allTasksModel.OrderOfItemShow;
            int taskCurrentPage = allTasksModel.CurrentPage;
            int taskNumberOfItemShow = allTasksModel.PageItemShow;

            int skipTasks = (taskCurrentPage - 1) * taskNumberOfItemShow;

            tasks = await dbContext.MyTasks
                    .Where(task => task.UserId == userId)
                    .GetOptionTasks(taskOption)
                    .GetPriorityFilterTasks(taskHighPriority, taskMediumPriority, taskLowPrioriry)
                    .OrderTasks(taskShowOrder)
                    .ToListAsync();

            allTasksModel.TotalPages = 
                (tasks.Count + taskNumberOfItemShow - 1) / taskNumberOfItemShow;
            
            tasks = tasks.Skip(skipTasks).Take(taskNumberOfItemShow).ToList();

            tasks ??= new List<TaskModel>();

            allTasksModel.Tasks = tasks;

            return allTasksModel;
        }
        public async Task DeleteATaskAsync(int taskId)
        {
            var task = await dbContext.MyTasks.FirstOrDefaultAsync(item => item.Id == taskId);
            if(task != null)
            {
                dbContext.MyTasks.Remove(task);
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<TaskModel> GetATaskAsync(int taskId)
        {
            var task = await dbContext.MyTasks.FirstOrDefaultAsync(item => item.Id == taskId);
            return task ?? new TaskModel();
        }
        public async Task<bool> UpdateATaskAsync(TaskModel updatedTask)
        {
            var oldTask = await dbContext.MyTasks.FirstOrDefaultAsync(item => item.Id == updatedTask.Id);

            if (oldTask == null) return false;
            if (updatedTask.MyTask == null) return false;
            if (updatedTask.DueDate != oldTask.DueDate
                && updatedTask.DueDate < DateTime.Now) {

                updatedTask.DueDate = oldTask.DueDate;

                return false;
            }

            oldTask.MyTask = updatedTask.MyTask;
            oldTask.Status = updatedTask.Status;
            oldTask.Priority = updatedTask.Priority;
            oldTask.DueDate = updatedTask.DueDate;
            oldTask.UpdatedDate = DateTime.Now;
            oldTask.Description = updatedTask.Description??"";

            await dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<SearchTasksModel> GetSearchTasksAsync(SearchTasksModel searchTasksModel, string userId)
        {
            List<TaskModel> tasks;
            searchTasksModel.Tasks = new List<TaskModel>();

            int srPriority = searchTasksModel.TaskPriority;
            int srOrder = searchTasksModel.OrderOfItemShow;
            int srOption = searchTasksModel.TaskStatus;
            string srText = searchTasksModel.SearchText;
            int srCurrentPage = searchTasksModel.CurrentPage;
            int srPgaeItemShow = searchTasksModel.PageItemShow;

            int skipeTasks = (srCurrentPage - 1) * srPgaeItemShow;

            tasks = await dbContext.MyTasks
                    .Where(task => task.UserId == userId)
                    .GetSearchOptionTasks(srOption)
                    .GetSearchPriorityFilterTasks(srPriority)
                    .GetPatternMatchingTasks(srText)
                    .OrderTasks(srOrder)
                    .ToListAsync();

            searchTasksModel.TotalPages = (tasks.Count + srPgaeItemShow - 1) / srPgaeItemShow;

            tasks = tasks.Skip(skipeTasks).Take(srPgaeItemShow).ToList();

            tasks ??= new List<TaskModel>();

            searchTasksModel.Tasks = tasks;

            return searchTasksModel;
        }
        
        public async Task GetAllUser(SearchUsersModel searchUsersModel)
        {
            string SearchText = searchUsersModel.SearchText;
            int skipUsers = (searchUsersModel.CurrentPage - 1) * searchUsersModel.PageItemShow;
            int userShow = searchUsersModel.PageItemShow;

            var users = await dbContext.Users.ToListAsync();
            if(SearchText != null) users = users
                    .Where(user => user.Name.Contains(SearchText, StringComparison.InvariantCultureIgnoreCase))
                    .ToList();

            searchUsersModel.TotalPages = 
                (users.Count + userShow - 1) / userShow;


            users = users.Skip(skipUsers).Take(userShow).ToList();

            var roles = await dbContext.Roles.ToListAsync();
            var userRoles = await dbContext.UserRoles.ToListAsync();

            foreach(var user in users)
            {
                var uRole = userRoles.FirstOrDefault(r => r.UserId == user.Id);
                if (uRole == null) user.RoleName = "User";
                else
                {
                    user.RoleId = uRole.RoleId;
                    user.RoleName = roles.FirstOrDefault(role => role.Id == uRole.RoleId).Name;
                }
            }
            searchUsersModel.Tasks = users ?? new List<UserModel>();
        }
        public async Task UpdateUserRole(string Email)
        {
            var dbUser = dbContext.Users.FirstOrDefault(u => u.Email == Email);
            if (dbUser == null) return;

            var dbUserRole = dbContext.UserRoles.FirstOrDefault(u => u.UserId == dbUser.Id);

            var prevRoleName = "User";

            if (dbUserRole != null)
            {
                prevRoleName = dbContext.Roles.Where(r => r.Id == dbUserRole.RoleId)
                .Select(e => e.Name).FirstOrDefault();
            }

            await _userManager.RemoveFromRoleAsync(dbUser, prevRoleName);
            await _userManager.AddToRoleAsync(dbUser,
                prevRoleName == "Admin" ? "User" : "Admin");

        }
        #endregion
    }
}
