using Microsoft.AspNetCore.Mvc;
using WebTravel.Attribute;

namespace Sheraton.Areas.Sale.Controllers
{
    [CheckRole("Sale")]
    [Area("Sale")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
