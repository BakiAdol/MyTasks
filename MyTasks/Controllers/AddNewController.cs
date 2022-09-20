using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;

namespace MyTasks.Controllers
{
    public class AddNewController : Controller
    {
        #region Props
        private readonly IMyTaskRepository myTaskService;
        #endregion

        #region Ctor
        public AddNewController(IMyTaskRepository myTaskService)
        {
            this.myTaskService = myTaskService;
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
            if (!ModelState.IsValid) return View();

            await myTaskService.AddNewTaskAsync(task);

            TempData["GetNotification"] = 0;

            return RedirectToAction("Index", "Tasks");
        }
        #endregion
    }
}
