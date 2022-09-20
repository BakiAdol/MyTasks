using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.Models;
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
        public async Task<IActionResult> Index(int taskId)
        {
            var task = await myTaskService.GetATaskAsync(taskId);
            
            return View(task);
        }
        [HttpPost]
        public async Task<IActionResult> Index(TaskModel task) // save edit task
        {
            bool isUpdateSuccessful = await myTaskService.UpdateATaskAsync(task);

            if(! isUpdateSuccessful) return View(task);

            TempData["GetNotification"] = 1;

            return RedirectToAction("Index", "Tasks");
        }
        public async Task<IActionResult> DeleteATask(int taskId)
        {
            await myTaskService.DeleteATaskAsync(taskId);

            TempData["GetNotification"] = 2;

            return RedirectToAction("Index", "Tasks");
        }
        #endregion
    }
}
