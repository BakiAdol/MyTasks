using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
using MyTasks.Services;

namespace MyTasks.Controllers
{
    public class AddNewController : Controller
    {
        #region Props
        private readonly IMyTaskService myTaskService;
        #endregion

        #region Ctor
        public AddNewController(IMyTaskService myTaskService)
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
        public IActionResult Index(TaskModel task)
        {
            if (!ModelState.IsValid) return View();

            myTaskService.AddNewTask(task);

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
