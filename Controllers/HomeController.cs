using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
using MyTasks.Services;
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