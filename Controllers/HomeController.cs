using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;

namespace Sheraton.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly SheratonDbContext _context;

        public HomeController(SheratonDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Product()
        {
            return View(await _context.SanhTiecs.ToListAsync());
        }
        public async Task<IActionResult> Experience()
        {
            return View();
        }
    }
}
