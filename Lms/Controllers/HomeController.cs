using Lms.Models;
using Lms.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lms.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            if(ClaimUtility.GetRolse(User).Where(p => p == "Student").Count() > 0)
            {
                return RedirectToAction(nameof(Index), "Student");
            }
            else if (ClaimUtility.GetRolse(User).Where(p => p == "Master").Count() > 0)
            {
                return RedirectToAction(nameof(Index),"Master");
            }
            else if (ClaimUtility.GetRolse(User).Where(p => p == "Admin").Count() > 0)
            {
                return RedirectToAction(nameof(Index), "Admin");
            }

            else
            {
                return null;
            }
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}