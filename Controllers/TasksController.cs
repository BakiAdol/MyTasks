using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(int? option, int page = 1, int show = 2, int order = 0,
            int high = 0, int medium = 0, int low = 0)
        {
            Pager pager = new Pager
            {
                CurrentPageNumber = page,
                PageItemShow = show,
                OrderOfItemShow = order,
                HighPriority = high,
                LowPriority = low,
                MediumPriority = medium
            };

            var tasks = myTaskService.GetAllTasks(pager, option);

            ViewBag.pager = pager;
            ViewBag.PageOption = option;

            return View(tasks);
        }
        #endregion
    }
}
