using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyTasksClassLib.Models;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using MyTasks.Services;
using MyTasks.Services.IServices;

namespace MyTasks.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        #region Props
        private readonly IMyTasksService _myTaskService;
        #endregion

        #region Ctor
        public SearchController(IMyTasksService myTaskService)
        {
            _myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> Index(SearchTasksModel? searchTasksModel)
        {
            if (searchTasksModel == null || searchTasksModel.SearchText == "")
            {
                return View(new SearchTasksModel());
            }

            searchTasksModel.PageItemShow = 5;

            await _myTaskService.GetSearchTasksAsync(searchTasksModel);
            
            searchTasksModel.ControllerName = "Search";
            searchTasksModel.ActionName = "Index";

            return View(searchTasksModel);
        }
        #endregion
    }
}
