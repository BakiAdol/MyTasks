using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.Models;

namespace MyTasks.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View(new RegisterModel());
        }
        [HttpPost]
        public IActionResult Registration(RegisterModel regInfo)
        {
            return View(new RegisterModel());
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginModel());
        }
        [HttpPost]
        public IActionResult Login(LoginModel loginInfo)
        {
            return View(new LoginModel());
        }
    }
}
