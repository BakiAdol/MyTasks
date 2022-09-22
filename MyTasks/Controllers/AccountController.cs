using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTasksClassLib.DataAccess.Migrations;
using MyTasksClassLib.Models;
using System.Security.Cryptography.Xml;

namespace MyTasks.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        #region Props
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        public readonly RoleManager<IdentityRole> _roleManager;
        #endregion

        #region Ctor
        public AccountController(UserManager<UserModel> userManager, 
            SignInManager<UserModel> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Methods
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Registration(string returnUrl = null)
        {
            if(! await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (!await _roleManager.RoleExistsAsync("User"))
            {
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }

            ViewData["returnUrl"] = returnUrl;

            return View(new RegisterModel());
        }
        
        [HttpPost]
        public async Task<IActionResult> Registration(RegisterModel regInfo, string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            returnUrl ??= Url.Content("~/");

            var user = new UserModel { UserName = regInfo.Email, Email = regInfo.Email, 
                Name = regInfo.Name};

            var res = await _userManager.CreateAsync(user, regInfo.Password);

            if(res.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                
                await _signInManager.SignInAsync(user, isPersistent: false);

                return LocalRedirect(returnUrl);
            }

            AddError(res);

            return View(regInfo);
        }
       
        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            ViewData["returnUrl"] = returnUrl;
            return View(new LoginModel());
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginInfo, string returnUrl = null)
        {
            ViewData["returnUrl"] = returnUrl;
            returnUrl ??= Url.Content("~/");

            var res = await _signInManager.PasswordSignInAsync(loginInfo.Email, loginInfo.Password,
                loginInfo.RememberMe, lockoutOnFailure: false);

            if(res.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");

            return View(loginInfo);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        private void AddError(IdentityResult res) 
        {
            foreach(var error in res.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        #endregion
    }
}
