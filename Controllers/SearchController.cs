using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        public IActionResult Index(string? SearchText, int? Option, int? Priority, int? Order, int page=1)
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

            Pager pager = new Pager
            {
                CurrentPageNumber = page,
                TotalPage = 1,
                PageItemShow = 5
            };

            var searchTasks = myTaskService.GetSearchTasks(searchInfo, pager);

            ViewBag.searchInfo = searchInfo;
            ViewBag.pager = pager;
            ViewBag.controller = "Search";
            ViewBag.action = "Index";

            return View(searchTasks);
        }
        #endregion

    }
}
