using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
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
        public IActionResult Index(int? options)
        {
            List<TaskModel> tasks = new List<TaskModel>();
            if (options == null)
            {
                tasks = myTaskService.GetAllTasks();
            }
            else if(options == 3)
            {
                tasks = myTaskService.GetOverDueTasks();
            }
            else if(options<4 && options>=0)
            {
                tasks = myTaskService.GetStatusTasks((int)options);
            }

            return View(tasks);
        }
        #endregion
    }
}