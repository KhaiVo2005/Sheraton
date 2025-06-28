using Microsoft.AspNetCore.Mvc;

namespace Sheraton.Areas.Accounting.Controllers
{
    public class HomeController : Controller
    {
        [Area("Accounting")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
