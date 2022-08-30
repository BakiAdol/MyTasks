using Microsoft.AspNetCore.Mvc;
using MyTasks.Services;
using System.Diagnostics;

namespace MyTasks.Controllers
{
    public class HomeController : Controller
    {
        #region Props
        private readonly IMyTaskService myTaskService;
        #endregion

        #region Ctor
        public HomeController(IMyTaskService myTaskService)
        {
            this.myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            var tasks = myTaskService.GetAllTasks();

            return View(tasks);
        }
        #endregion
    }
}