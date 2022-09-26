using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using MyTasks.Services.IServices;
using System.Threading.Tasks;

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
            var tModel = new TaskModel();
            var userID = userService.GetUserId();

            tModel.UserId = userService.GetUserId();

            return View(tModel);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(TaskModel task)
        {
            if (!ModelState.IsValid) return View();

            task.Description ??= string.Empty;

            await myTaskService.AddNewTaskAsync(task);

            TempData["GetNotification"] = 0;

            return RedirectToAction("Index", "Tasks");
        }
        #endregion
    }
}
