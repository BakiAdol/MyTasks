using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using MyTasks.Services.IServices;

namespace MyTasks.Controllers
{
    [Authorize]
    public class AddNewController : Controller
    {
        #region Props
        private readonly IMyTaskRepository myTaskService;
        private readonly IUserService userService;
        #endregion

        #region Ctor
        public AddNewController(IMyTaskRepository myTaskService, IUserService userService)
        {
            this.myTaskService = myTaskService;
            this.userService = userService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(TaskModel task)
        {
            // if (!ModelState.IsValid) return View();

            var userID = userService.GetUserId();

            task.UserId = userService.GetUserId();

            await myTaskService.AddNewTaskAsync(task);

            TempData["GetNotification"] = 0;

            return RedirectToAction("Index", "Tasks");
        }
        #endregion
    }
}
