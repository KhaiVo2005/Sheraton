using Microsoft.AspNetCore.Mvc;
using WebTravel.Attribute;

namespace Sheraton.Areas.Banquet.Controllers
{
    [CheckRole("Banquet")]
    public class HomeController : Controller
    {

        [Area("Banquet")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
