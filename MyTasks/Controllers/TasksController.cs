using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using MyTasks.Services.IServices;

namespace MyTasks.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        #region Props
        private readonly IMyTasksService _myTaskService;
        #endregion

        #region Ctor
        public TasksController(IMyTasksService myTaskService)
        {
            _myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> Index(AllTasksModel? allTasksModel)
        {
            allTasksModel ??= new AllTasksModel();

            await _myTaskService.GetAllTasksAsync(allTasksModel);

            allTasksModel.ControllerName = "Tasks";
            allTasksModel.ActionName = "Index";

            return View(allTasksModel);
        }
        #endregion
    }
}
