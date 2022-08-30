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
            else if(options == 4)
            {
                tasks = myTaskService.GetOverDueTasks();
            }

            return View(tasks);
        }
        public IActionResult DeleteATask(int taskId)
        {
            myTaskService.DeleteATask(taskId);

            return RedirectToAction("Index");
        }
        #endregion
    }
}