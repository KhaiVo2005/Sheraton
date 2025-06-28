using Microsoft.AspNetCore.Mvc;

namespace Sheraton.Areas.Banquet.Controllers
{
    public class HomeController : Controller
    {
        [Area("Banquet")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
