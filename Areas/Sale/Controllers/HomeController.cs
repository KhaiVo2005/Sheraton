using Microsoft.AspNetCore.Mvc;

namespace Sheraton.Areas.Sale.Controllers
{
    [Area("Sale")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
