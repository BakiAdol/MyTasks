using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MyTasksClassLib.Models.ViewModels;
using MyTasks.Services.IServices;

namespace MyTasks.Controllers
{
    [Authorize]
    public class DetailsController : Controller
    {
        #region Props
        private readonly IMyTaskRepository _myTaskRepository;
        private readonly IMyTasksService _myTasksService;
        #endregion

        #region Ctor
        public DetailsController(IMyTaskRepository myTaskService, IMyTasksService myTasksService)
        {
            _myTaskRepository = myTaskService;
            _myTasksService = myTasksService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index(int taskId)
        {
            var taskDetail = await _myTasksService.GetATaskDetailServiceAsync(taskId);
            
            return View(taskDetail);
        }
        [HttpPost]
        public async Task<IActionResult> Index(DetailViewModel updatedTask) // save edit task
        {
            await _myTasksService.UpdateTaskServiceAsync(updatedTask);

            TempData["GetNotification"] = 1;

            return RedirectToAction("Index", "Tasks");
        }
        public async Task<IActionResult> DeleteATask(int taskId)
        {
            await _myTaskRepository.DeleteATaskAsync(taskId);

            TempData["GetNotification"] = 2;

            return RedirectToAction("Index", "Tasks");
        }
        #endregion
    }
}
