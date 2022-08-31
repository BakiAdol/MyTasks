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
        public IActionResult DeleteATask(int taskId)
        {
            myTaskService.DeleteATask(taskId);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult SaveEditTask(TaskModel task)
        {
            myTaskService.UpdateATask(task);

            return RedirectToAction("Index", "Home");
        }
        #endregion
    }
}
