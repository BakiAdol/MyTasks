using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Services.IServices;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using MyTasksClassLib.Models;
using System.Data;

namespace MyTasks.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        #region Props
        private readonly IMyTaskRepository myTaskService;
        #endregion

        #region Ctor
        public AdminController(IUserService userService, IMyTaskRepository myTaskService)
        {
            _userService = userService;
            this.myTaskService = myTaskService;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList(SearchUsersModel? searchUsersModel)
        {
            searchUsersModel ??= new SearchUsersModel();

            searchUsersModel.PageItemShow = 5;

            await _userService.GetAllUserAsync(searchUsersModel);

            searchUsersModel.ControllerName = "Admin";
            searchUsersModel.ActionName = "UserList";

            return View(searchUsersModel);
        }

        public async Task<IActionResult> ChangeRole(string Email)
        {
            await myTaskService.UpdateUserRole(Email);

            return RedirectToAction("UserList");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}
