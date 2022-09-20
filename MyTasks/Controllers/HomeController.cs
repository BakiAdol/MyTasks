using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.Models;
using System.Diagnostics;

namespace MyTasks.Controllers
{
    public class HomeController : Controller
    {
        #region Methods
        public IActionResult Index()
        {
            return View();
        }
        #endregion
    }
}