using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
using MyTasks.Services;

namespace MyTasks.Controllers
{
    public class SearchController : Controller
    {
        #region Props
        private readonly IMyTaskService myTaskService;
        #endregion

        #region Ctor
        public SearchController(IMyTaskService myTaskService)
        {
            this.myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public IActionResult Index(string? SearchText, int? Option, int? Priority, int? Order)
        {
            if(SearchText == null || Option == null || Priority == null || Order == null)
            {
                ViewBag.searchInfo = new SearchModel();
                return View(new List<TaskModel>());
            }

            var searchInfo = new SearchModel
            {
                SearchText = SearchText,
                Option = (int)Option,
                Priority = (int)Priority,
                Order = (int)Order
            };

            var searchTasks = myTaskService.GetSearchTasks(searchInfo);

            ViewBag.searchInfo = searchInfo;

            return View(searchTasks);
        }
        #endregion

    }
}
