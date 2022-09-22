using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.DataAccess.Repository.IRepository;
using System.Data;

namespace MyTasks.Controllers
{
    public class AdminController : Controller
    {
        private readonly IMyTaskRepository myTaskService;

        public AdminController(IMyTaskRepository myTaskService)
        {
            this.myTaskService = myTaskService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList()
        {
            var users = await myTaskService.GetAllUser();

            return View(users);
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
