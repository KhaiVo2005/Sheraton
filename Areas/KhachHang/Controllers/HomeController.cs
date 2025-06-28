using Microsoft.AspNetCore.Mvc;

namespace Sheraton.Areas.KhachHang.Controllers
{
    [Area("KhachHang")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
