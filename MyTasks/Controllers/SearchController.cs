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
        private readonly IMyTaskRepository myTaskService;
        private readonly IUserService userService;
        #endregion

        #region Ctor
        public SearchController(IMyTaskRepository myTaskService, IUserService userService)
        {
            this.myTaskService = myTaskService;
            this.userService = userService;
        }
        #endregion

        #region Methods
        [HttpGet]
        public async Task<IActionResult> Index(SearchTasksModel? searchTasksModel)
        {
            var userId = userService.GetUserId();

            if (searchTasksModel == null || searchTasksModel.SearchText == "")
            {
                return View(new SearchTasksModel());
            }

            searchTasksModel.PageItemShow = 5;

            searchTasksModel = await myTaskService.GetSearchTasksAsync(searchTasksModel, userId);
            
            searchTasksModel.ControllerName = "Search";
            searchTasksModel.ActionName = "Index";

            return View(searchTasksModel);
        }
        #endregion

    }
}
