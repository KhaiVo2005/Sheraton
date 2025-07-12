using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Helpers;
using Sheraton.Models;
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
                .Include(h => h.DichVu)
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
                    .Where(h => h.DichVu != null)
                    .Sum(h => h.DichVu.DonGia);

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
        public async Task<IActionResult> QuanLyHoaDon()
        {
            var hopDongs = await _context.HopDongs
                .Include(h => h.ChiTietDatTiecs).ThenInclude(c => c.MonAn)
                .Include(h => h.DichVu)
                .Include(h => h.LichDatSanhs).ThenInclude(l => l.Sanh)
                .Include(h => h.DichVu)
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .ToListAsync();

            var list = new List<HoaDon>();

            foreach (var hd in hopDongs)
            {
                var hoaDon = new HoaDon
                {
                    MaHD = hd.MaHD,
                    TenKhachHang = hd.KhachHang.TenKH,
                    TenNhanVien = hd.NhanVien.TenNV,
                    NgayKy = hd.NgayKy,
                    TienCoc = hd.TienCoc,
                    TienSanh = hd.LichDatSanhs.Sum(c => c.Sanh.Gia),
                    TienMonAn = hd.ChiTietDatTiecs.Sum(c => c.SoLuong * (c.MonAn?.DonGia ?? 0)),
                    TienDichVu = hd.DichVu?.DonGia ?? 0,
                    TrangThai = hd.TrangThai
                };
                list.Add(hoaDon);
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult XuLyThanhToan(Guid maHD, string hinhThuc)
        {
            if (hinhThuc == "TrucTiep")
            {
                var hopDong = _context.HopDongs.Find(maHD);
                if (hopDong != null && hopDong.TrangThai == "Chưa thanh toán")
                {
                    hopDong.TrangThai = "Đã thanh toán";
                    _context.SaveChanges();
                }

                return RedirectToAction("detailsHoaDon", new { id = maHD });
            }
            else if (hinhThuc == "Online")
            {
                var hopDong = _context.HopDongs
                    .Include(h => h.ChiTietDatTiecs).ThenInclude(c => c.MonAn)
                    .Include(h => h.DichVu)
                    .Include(h => h.LichDatSanhs).ThenInclude(l => l.Sanh)
                    .Include(h => h.KhachHang)
                    .Include(h => h.NhanVien)
                    .FirstOrDefault(h => h.MaHD == maHD);

                if (hopDong == null) return NotFound();

                var TienCoc = hopDong.TienCoc;
                var TienSanh = hopDong.LichDatSanhs.Sum(c => c.Sanh.Gia);
                var TienMonAn = hopDong.ChiTietDatTiecs.Sum(c => c.SoLuong * (c.MonAn?.DonGia ?? 0));
                var TienDichVu = hopDong.DichVu.DonGia;

                decimal tongTien = TienCoc + TienSanh + TienMonAn + TienDichVu;

                string description = $"Thanh toan HD: {hopDong.MaHD}";

                using (var client = new HttpClient())
                {
                    var url = $"https://localhost:7136/api/Vnpay/CreatePaymentUrl?moneyToPay={tongTien}&description={Uri.EscapeDataString(description)}&maHD={hopDong.MaHD}";
                    var paymentUrl = client.GetStringAsync(url).Result;
                    return Redirect(paymentUrl); 
                }

            }

            return RedirectToAction("detailsHoaDon", new { id = maHD });
        }

        public async Task<IActionResult> detailsHoaDon(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hopDong = await _context.HopDongs
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.LichDatSanhs).ThenInclude(c => c.Sanh)
                .Include(h => h.ChiTietDatTiecs).ThenInclude(c => c.MonAn)
                .Include(h => h.DichVu)
                .FirstOrDefaultAsync(h => h.MaHD == id);

            decimal tienMonAn = hopDong.ChiTietDatTiecs?.Sum(c => c.SoLuong * (c.MonAn?.DonGia ?? 0)) ?? 0;

            // ✅ Tính tiền dịch vụ
            decimal tienDichVu = hopDong.DichVu?.DonGia ?? 0;

            // ✅ Tính tiền sảnh
            decimal tienSanh = hopDong.LichDatSanhs?.FirstOrDefault()?.Sanh?.Gia ?? 0;
            var hoaDon = new HoaDon
            {
                MaHD = hopDong.MaHD,
                TenKhachHang = hopDong.KhachHang.TenKH,
                TenNhanVien = hopDong.NhanVien.TenNV,
                TenDichVu = hopDong.DichVu.TenDV,
                NgayKy = hopDong.NgayKy,
                TienCoc = hopDong.TienCoc,
                TienMonAn = tienMonAn,
                TienDichVu = tienDichVu,
                TienSanh = tienSanh,
                TrangThai = hopDong.TrangThai,
                hopDong = hopDong,
                chiTietDatTiecs = hopDong.ChiTietDatTiecs?.ToList() ?? new()
            };


            if (hoaDon == null)
            {
                return NotFound();
            }

            return View(hoaDon);
        }

        public async Task<IActionResult> ExportHoaDonToPdf(Guid id, [FromServices] PdfRenderHelper pdfHelper)
        {
            var hopDong = await _context.HopDongs
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.LichDatSanhs).ThenInclude(c => c.Sanh)
                .Include(h => h.ChiTietDatTiecs).ThenInclude(c => c.MonAn)
                .Include(h => h.DichVu)
                .FirstOrDefaultAsync(h => h.MaHD == id);

            decimal tienMonAn = hopDong.ChiTietDatTiecs?.Sum(c => c.SoLuong * (c.MonAn?.DonGia ?? 0)) ?? 0;

            // ✅ Tính tiền dịch vụ
            decimal tienDichVu = hopDong.DichVu?.DonGia ?? 0;

            // ✅ Tính tiền sảnh
            decimal tienSanh = hopDong.LichDatSanhs?.FirstOrDefault()?.Sanh?.Gia ?? 0;
            var hoaDon = new HoaDon
            {
                MaHD = hopDong.MaHD,
                TenKhachHang = hopDong.KhachHang.TenKH,
                TenNhanVien = hopDong.NhanVien.TenNV,
                TenDichVu = hopDong.DichVu.TenDV,
                NgayKy = hopDong.NgayKy,
                TienCoc = hopDong.TienCoc,
                TienMonAn = tienMonAn,
                TienDichVu = tienDichVu,
                TienSanh = tienSanh,
                TrangThai = hopDong.TrangThai,
                hopDong = hopDong,
                chiTietDatTiecs = hopDong.ChiTietDatTiecs?.ToList() ?? new()
            };


            // 1️⃣ Render Razor View thành HTML
            string htmlContent = await pdfHelper.RenderViewAsync(this, "/Areas/Accounting/Views/Home/Export.cshtml", hoaDon);

            // 2️⃣ Gọi API Python (http://localhost:5000/api/pdf)
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync("http://localhost:5000/api/pdf", new { html = htmlContent });

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Không thể tạo PDF");

            // 3️⃣ Trả file PDF cho trình duyệt
            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", "HoaDon.pdf");
        }
    }
}
