using Microsoft.AspNetCore.Mvc;

namespace MyTasks.Controllers
{
    public class AddNewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
