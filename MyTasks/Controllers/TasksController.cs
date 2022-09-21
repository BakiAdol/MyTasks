using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;

namespace MyTasks.Controllers
{
    public class TasksController : Controller
    {
        #region Props
        private readonly IMyTaskRepository myTaskService;
        #endregion

        #region Ctor
        public TasksController(IMyTaskRepository myTaskService)
        {
            this.myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        [Authorize]
        public async Task<IActionResult> Index(AllTasksModel? allTasksModel)
        {
            if (allTasksModel == null)
            {
                allTasksModel = new AllTasksModel();
            }

            var tasks = await myTaskService.GetAllTasksAsync(allTasksModel);

            allTasksModel.ControllerName = "Tasks";
            allTasksModel.ActionName = "Index";

            return View(allTasksModel);
        }
        #endregion
    }
}
