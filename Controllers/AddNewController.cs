using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;

namespace MyTasks.Controllers
{
    public class AddNewController : Controller
    {
        #region Methods
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(TaskModel task)
        {
            return View();
        }
        #endregion
    }
}
