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
        private readonly IMyTasksService _myTasksService;
        #endregion

        #region Ctor
        public DetailsController(IMyTasksService myTasksService)
        {
            _myTasksService = myTasksService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index(int taskId)
        {
            var taskDetail = await _myTasksService.GetATaskDetailAsync(taskId);
            
            return View(taskDetail);
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(DetailViewModel updatedTask)
        {
            await _myTasksService.UpdateTaskAsync(updatedTask);

            TempData["GetNotification"] = 1;

            return RedirectToAction("Index", "Tasks");
        }
        
        public async Task<IActionResult> DeleteATask(int taskId)
        {
            await _myTasksService.DeleteTaskAsync(taskId);

            TempData["GetNotification"] = 2;

            return RedirectToAction("Index", "Tasks");
        }
        #endregion
    }
}
