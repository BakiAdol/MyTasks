using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using MyTasks.Services.IServices;
using System.Threading.Tasks;
using MyTasksClassLib.Models.ViewModels;

namespace MyTasks.Controllers
{
    [Authorize]
    public class AddNewController : Controller
    {
        #region Props
        private readonly IMyTasksService myTaskService;
        private readonly IUserService userService;
        #endregion

        #region Ctor
        public AddNewController(IMyTasksService myTaskService, IUserService userService)
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
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(AddNewViewModel newTask)
        {
            if (!ModelState.IsValid) return View();

            var userID = userService.GetUserId();

            await myTaskService.AddNewTaskServiceAsync(newTask, userID);

            TempData["GetNotification"] = 0;

            return RedirectToAction("Index", "Tasks");
        }
        #endregion
    }
}
