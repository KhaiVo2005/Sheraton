using Microsoft.AspNetCore.Mvc;
using WebTravel.Attribute;

namespace Sheraton.Areas.HumanResources.Controllers
{
    public class HomeController : Controller
    {
        [CheckRole("HumanResources")]
        [Area("HumanResources")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
