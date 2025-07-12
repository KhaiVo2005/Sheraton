using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Helpers;
using Sheraton.Models;
using Sheraton.Models.ViewModel;

namespace Sheraton.Areas.Sale.Controllers
{
    [Area("Sale")]
    public class HopDongsController : Controller
    {
        private readonly SheratonDbContext _context;

        public HopDongsController(SheratonDbContext context)
        {
            _context = context;
        }

        // GET: Sale/HopDongs
        public async Task<IActionResult> getHopDong()
        {
            var sheratonDbContext = _context.HopDongs.Include(h => h.KhachHang).Include(h => h.NhanVien);

            return View(await sheratonDbContext.ToListAsync());
        }

        // GET: Sale/HopDongs/Details/5
        public async Task<IActionResult> detailsHopDong(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.DichVu)
                .Include(h => h.LichDatSanhs)
                .FirstOrDefaultAsync(m => m.MaHD == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // GET: Sale/HopDongs/Create
        // GET: Sale/HopDongs/Create
        public IActionResult createHopDong()
        {
            // Danh sách khách hàng hiển thị cả tên và email
            var khachHangs = _context.KhachHangs
                .Select(kh => new
                {
                    MaKH = kh.MaKH,
                    ThongTin = kh.TenKH + " - " + kh.Email
                }).ToList();

            // Danh sách nhân viên hiển thị cả tên và chức vụ
            var nhanViens = _context.NhanViens
                .Select(nv => new
                {
                    MaNV = nv.MaNV,
                    ThongTin = nv.TenNV + " - " + nv.ChucVu
                }).ToList();

            var dichVus = _context.DichVus
                .Select(dv => new
                {
                    MaDV = dv.MaDV,
                    ThongTin = dv.TenDV
                }).ToList();

            ViewData["MaKH"] = new SelectList(khachHangs, "MaKH", "ThongTin");
            ViewData["MaNV"] = new SelectList(nhanViens, "MaNV", "ThongTin");
            ViewData["MaDV"] = new SelectList(dichVus, "MaDV", "ThongTin");

            var danhSachSanh = _context.SanhTiecs.ToList();
            var dsTrangThai = danhSachSanh.Select(s => new SanhTiec
            {
                MaSanh = s.MaSanh,
                TenSanh = s.TenSanh,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai
            }).ToList();

            var lichDaDat = _context.LichDatSanhs
                .Select(l => new
                {
                    maSanh = l.MaSanh,
                    batDau = l.BatDau.ToString("o"),
                    ketThuc = l.KetThuc.ToString("o")

                }).ToList();


            ViewBag.DanhSachSanh = dsTrangThai;
            ViewBag.LichDaDat = lichDaDat;

            var danhsachmonan = _context.MonAns.ToList();
            ViewBag.DanhSachMonAn = danhsachmonan;

            return View(new HopDongLichDat());
        }




        // POST: Sale/HopDongs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Sale/HopDongs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> createHopDong(HopDongLichDat model)
        {
            if (ModelState.IsValid)
            {
                model.HopDong.MaHD = Guid.NewGuid();
                _context.Add(model.HopDong);

                model.LichDatSanh.MaLDS = Guid.NewGuid();
                model.LichDatSanh.MaHD = model.HopDong.MaHD;
                _context.Add(model.LichDatSanh);

                foreach (var monAn in model.monAns)
                {
                    if (monAn != null && monAn.SoLuong > 0)
                    {
                        monAn.MaHD = model.HopDong.MaHD;
                        _context.Add(monAn);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("getHopDong", "HopDongs", new { maHD = model.HopDong.MaHD });
            }

            // Load lại dropdown nếu không hợp lệ
            ViewData["MaKH"] = new SelectList(_context.KhachHangs.Select(kh => new
            {
                MaKH = kh.MaKH,
                ThongTin = kh.TenKH + " - " + kh.Email
            }), "MaKH", "ThongTin", model.HopDong.MaKH);

            ViewData["MaNV"] = new SelectList(_context.NhanViens.Select(nv => new
            {
                MaNV = nv.MaNV,
                ThongTin = nv.TenNV + " - " + nv.ChucVu
            }), "MaNV", "ThongTin", model.HopDong.MaNV);

            ViewData["MaDV"] = new SelectList(_context.DichVus.Select(dv => new
            {
                MaDV = dv.MaDV,
                ThongTin = dv.TenDV
            }), "MaDV", "ThongTin", model.HopDong.MaNV);

            var lichDaDatGoc = _context.LichDatSanhs.ToList();
            var dsSanh = _context.SanhTiecs.ToList();

            var dsTrangThai = dsSanh.Select(s =>
            {
                var biTrung = lichDaDatGoc.Any(ld =>
                    ld.MaSanh == s.MaSanh &&
                    model.LichDatSanh.BatDau < ld.KetThuc &&
                    model.LichDatSanh.KetThuc > ld.BatDau);

                string trangThai = s.TrangThai;
                if (biTrung) trangThai = "Đã đặt";

                return new SanhTiec
                {
                    MaSanh = s.MaSanh,
                    TenSanh = s.TenSanh,
                    SucChua = s.SucChua,
                    TrangThai = trangThai
                };
            }).ToList();

            ViewBag.DanhSachSanh = dsTrangThai;

            // 👇 Thêm dòng này để JavaScript trong View nhận được dữ liệu lịch đã đặt
            ViewBag.LichDaDat = lichDaDatGoc.Select(l => new
            {
                maSanh = l.MaSanh,
                batDau = l.BatDau.ToString("o"),
                ketThuc = l.KetThuc.ToString("o")
            }).ToList();

            ViewBag.DanhSachMonAn = _context.MonAns.ToList();

            return View(model);
        }



        // GET: Sale/HopDongs/Edit/5
        public async Task<IActionResult> updateHopDong(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound();
            }
            ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "TenKH", hopDong.MaKH);
            ViewData["MaNV"] = new SelectList(_context.NhanViens, "MaNV", "ChucVu", hopDong.MaNV);
            ViewData["MaDV"] = new SelectList(_context.NhanViens, "MaDV", "TenDV", hopDong.MaNV);
            return View(hopDong);
        }

        // POST: Sale/HopDongs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateHopDong(Guid id, [Bind("MaHD,NgayKy,TienCoc,TrangThai,MaKH,MaNV,MaDV")] HopDong hopDong)
        {
            if (id != hopDong.MaHD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hopDong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HopDongExists(hopDong.MaHD))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(getHopDong));
            }
            ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "Email", hopDong.MaKH);
            ViewData["MaNV"] = new SelectList(_context.NhanViens, "MaNV", "ChucVu", hopDong.MaNV);
            ViewData["MaDV"] = new SelectList(_context.DichVus, "MaDV", "TenDV", hopDong.MaDV);

            return View(hopDong);
        }

        // GET: Sale/HopDongs/Delete/5
        public async Task<IActionResult> deleteHopDong(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.DichVu)
                .FirstOrDefaultAsync(m => m.MaHD == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // POST: Sale/HopDongs/Delete/5
        [HttpPost, ActionName("deleteHopDong")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDongs.Remove(hopDong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(getHopDong));
        }

        private bool HopDongExists(Guid id)
        {
            return _context.HopDongs.Any(e => e.MaHD == id);
        }
        public async Task<IActionResult> ExportHopDongToPdf(Guid id, [FromServices] PdfRenderHelper pdfHelper)
        {
            var hopDong = await _context.HopDongs
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .Include(h => h.DichVu)
                .Include(h => h.LichDatSanhs)
                .FirstOrDefaultAsync(h => h.MaHD == id);

            if (hopDong == null) return NotFound();

            // 1️⃣ Render Razor View thành HTML
            string htmlContent = await pdfHelper.RenderViewAsync(this, "/Areas/Sale/Views/HopDongs/Export.cshtml", hopDong);

            // 2️⃣ Gọi API Python (http://localhost:5000/api/pdf)
            using var httpClient = new HttpClient();
            var response = await httpClient.PostAsJsonAsync("http://localhost:5000/api/pdf", new { html = htmlContent });

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode, "Không thể tạo PDF");

            // 3️⃣ Trả file PDF cho trình duyệt
            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", "HopDong.pdf");
        }


    }
}
