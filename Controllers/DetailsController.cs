using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
using MyTasks.Services;
using System.Threading.Tasks;

namespace MyTasks.Controllers
{
    public class DetailsController : Controller
    {
        #region Props
        private readonly IMyTaskService myTaskService;
        #endregion

        #region Ctor
        public DetailsController(IMyTaskService myTaskService)
        {
            this.myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        public IActionResult Index(int taskId)
        {
            var task = myTaskService.GetATask(taskId);
            
            return View(task);
        }
        [HttpPost]
        public IActionResult Index(TaskModel task) // save edit task
        {
            bool isUpdateSuccessful = myTaskService.UpdateATask(task);

            if(! isUpdateSuccessful) return View(task);

            TempData["GetNotification"] = 1;

            return RedirectToAction("Index", "Tasks");
        }
        public IActionResult DeleteATask(int taskId)
        {
            myTaskService.DeleteATask(taskId);

            TempData["GetNotification"] = 2;

            return RedirectToAction("Index", "Tasks");
        }
        #endregion
    }
}
