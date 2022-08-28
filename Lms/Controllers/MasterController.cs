using Microsoft.AspNetCore.Mvc;

namespace Lms.Controllers
{
    public class MasterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
