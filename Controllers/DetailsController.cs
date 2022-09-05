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
            if (!ModelState.IsValid) return View(task);

            myTaskService.UpdateATask(task);

            return RedirectToAction("Index", "Home");
        }
        public IActionResult DeleteATask(int taskId)
        {
            myTaskService.DeleteATask(taskId);

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
