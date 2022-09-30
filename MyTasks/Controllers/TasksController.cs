using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using MyTasks.Services.IServices;

namespace MyTasks.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        #region Props
        private readonly IMyTaskRepository myTaskService;
        private readonly IUserService userService;
        #endregion

        #region Ctor
        public TasksController(IMyTaskRepository myTaskService, IUserService userService)
        {
            this.myTaskService = myTaskService;
            this.userService = userService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index(AllTasksModel? allTasksModel)
        {
            var userId = userService.GetUserId();

            allTasksModel ??= new AllTasksModel();
            await myTaskService.GetAllTasksAsync(allTasksModel, userId);

            allTasksModel.ControllerName = "Tasks";
            allTasksModel.ActionName = "Index";

            return View(allTasksModel);
        }
        #endregion
    }
}
