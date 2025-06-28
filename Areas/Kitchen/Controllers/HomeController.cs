using Microsoft.AspNetCore.Mvc;

namespace Sheraton.Areas.Kitchen.Controllers
{
    public class HomeController : Controller
    {
        [Area("Kitchen")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
