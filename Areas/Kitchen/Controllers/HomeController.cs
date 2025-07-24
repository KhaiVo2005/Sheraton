using Microsoft.AspNetCore.Mvc;
using WebTravel.Attribute;

namespace Sheraton.Areas.Kitchen.Controllers
{
    public class HomeController : Controller
    {
        [CheckRole("Kitchen")]
        [Area("Kitchen")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
