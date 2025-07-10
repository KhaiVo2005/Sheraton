using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Models.ViewModel;

namespace Sheraton.Areas.Accounting.Controllers
{
    [Area("Accounting")]
    public class HomeController : Controller
    {
        SheratonDbContext _context;
        public HomeController(SheratonDbContext context)
        {
            _context = context;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ThongKeDoanhThu()
        {
            // Lấy toàn bộ dữ liệu cần
            var hopDongs = await _context.HopDongs
                .Include(h => h.ChiTietDatTiecs).ThenInclude(c => c.MonAn)
                .Include(h => h.ChiTietDichVus).ThenInclude(c => c.DichVu)
                .ToListAsync();

            var lichDatSanhs = await _context.LichDatSanhs
                .Include(l => l.Sanh)
                .ToListAsync();

            var list = new List<DoanhThuThang>();

            for (int thang = 1; thang <= 12; thang++)
            {
                var hds = hopDongs.Where(h => h.NgayKy.Month == thang);

                decimal tienCoc = hds.Sum(h => h.TienCoc);

                // Ghép sang sảnh
                decimal tienSanh = hds
                    .Join(lichDatSanhs,
                        hd => hd.MaHD,
                        lds => lds.MaHD,
                        (hd, lds) => lds.Sanh?.Gia ?? 0)
                    .Sum();

                // Món ăn
                decimal tienMonAn = hds
                    .SelectMany(h => h.ChiTietDatTiecs)
                    .Sum(c => c.SoLuong * (c.MonAn?.DonGia ?? 0));

                // Dịch vụ
                decimal tienDichVu = hds
                    .SelectMany(h => h.ChiTietDichVus)
                    .Sum(c => c.SoLuong * (c.DichVu?.DonGia ?? 0));

                decimal tong = tienCoc + tienSanh + tienMonAn + tienDichVu;

                list.Add(new DoanhThuThang
                {
                    Thang = thang,
                    TienCoc = tienCoc,
                    TienSanh = tienSanh,
                    TienMonAn = tienMonAn,
                    TienDichVu = tienDichVu,
                    Tong = tong
                });
            }

            return View(list);
        }
    }
}
