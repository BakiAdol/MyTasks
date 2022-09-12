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
        public IActionResult Index()
        {
            SearchModelMembers searchModelMembers = new SearchModelMembers();
            searchModelMembers.TaskModel = new List<TaskModel>();

            return View(searchModelMembers);
        }
        [HttpPost]
        public IActionResult Index(SearchModelMembers getInfo)
        {
            SearchModelMembers searchModelMembers = new SearchModelMembers();

            searchModelMembers.SearchModel = getInfo.SearchModel;

            searchModelMembers.TaskModel = new List<TaskModel>();

            if(getInfo?.SearchModel?.SearchText == null) return View(searchModelMembers);

            searchModelMembers.TaskModel = myTaskService.GetSearchTasks(getInfo.SearchModel);

            return View(searchModelMembers);
        }
        #endregion

    }
}
