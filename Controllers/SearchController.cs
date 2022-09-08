using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;

namespace MyTasks.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View(new List<TaskModel>());
        }
        [HttpPost]
        public IActionResult Index(SearchModel searchInfo)
        {
            if(!ModelState.IsValid) return View();

            return View();
        }
    }
}
