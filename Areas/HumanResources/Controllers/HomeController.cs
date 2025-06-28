using Microsoft.AspNetCore.Mvc;

namespace Sheraton.Areas.HumanResources.Controllers
{
    public class HomeController : Controller
    {
        [Area("HumanResources")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
