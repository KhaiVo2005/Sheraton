using Microsoft.AspNetCore.Mvc;

namespace Sheraton.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
