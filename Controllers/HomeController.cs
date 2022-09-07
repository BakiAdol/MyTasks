using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
using MyTasks.Services;
using System.Diagnostics;

namespace MyTasks.Controllers
{
    public class HomeController : Controller
    {
        #region Props
        private readonly IMyTaskService myTaskService;
        #endregion

        #region Ctor
        public HomeController(IMyTaskService myTaskService)
        {
            this.myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        public IActionResult Index(int? option, int page=1, int show=3, int order=0)
        {
            Pager pager = new Pager
            {
                CurrentPageNumber = page,
                PageItemShow = show,
                OrderOfItemShow = order
            };

            if(option == null)
            {
                var tasks = myTaskService.GetAllTasks(pager);

                ViewBag.pager = pager;

                return View(tasks);
            }
            return View( new List<TaskModel>());
        }
        #endregion
    }
}