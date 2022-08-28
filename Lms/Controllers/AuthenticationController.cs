using Lms.Models.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lms.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> signInManager;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Signin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Signin(SigninViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = _userManager.FindByNameAsync(model.UserName).Result;
            if (user == null)
            {
                ModelState.AddModelError("", "کاربر یافت نشد");
                return View(model);
            }
            signInManager.SignOutAsync();
            var result = signInManager.PasswordSignInAsync(user, model.Password
                , true, true).Result;

            if (result.Succeeded)
            {
                return Redirect("home/index");
            }

            ModelState.AddModelError("", "پسورد اشتباه است");
            return View(model);
        }

        public IActionResult Signup()
        {
            return Ok();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
