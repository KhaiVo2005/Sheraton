using Microsoft.AspNetCore.Mvc;
using Sheraton.Data;

namespace Sheraton.Controllers
{
    public class AccountController : Controller
    {
        private readonly SheratonDbContext _context;

        public AccountController(SheratonDbContext context)
        {
            _context = context;
        }

        // 👉 Trang login (GET)
        public IActionResult Login()
        {
            return View();
        }

        // 👉 Xử lý đăng nhập (POST)
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Tìm người dùng theo username và password
            var user = _context.NhanViens
                .FirstOrDefault(u => u.TK == username && u.MK == password);

            if (user != null)
            {
                // Lưu session
                HttpContext.Session.SetString("Id", user.MaNV.ToString());
                HttpContext.Session.SetString("Username", user.TK);
                HttpContext.Session.SetString("FullName", user.TenNV);
                HttpContext.Session.SetString("Role", user.ChucVu);

                // 👉 Điều hướng đến Area tương ứng theo Role
                return RedirectToAction("Index", "Home", new { area = user.ChucVu });
            }

            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
