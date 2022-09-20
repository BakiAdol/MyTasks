using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasks.Models;
using MyTasks.Services;

namespace MyTasks.Controllers
{
    public class TasksController : Controller
    {
        #region Props
        private readonly IMyTaskService myTaskService;
        #endregion

        #region Ctor
        public TasksController(IMyTaskService myTaskService)
        {
            this.myTaskService = myTaskService;
        }
        #endregion

        #region Methods
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
