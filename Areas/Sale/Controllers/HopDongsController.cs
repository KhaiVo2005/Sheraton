using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Helpers;
using Sheraton.Models;
using Sheraton.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebTravel.Attribute;

namespace Sheraton.Areas.Sale.Controllers
{
    [CheckRole("Sale")]
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
                .Include(h => h.LichDatSanhs).ThenInclude(h => h.Sanh)
                .Include(h => h.ChiTietDatTiecs).ThenInclude(c => c.MonAn)
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
                    batDau = l.BatDau.ToString("yyyy-MM-ddTHH:mm:ss"),
                    ketThuc = l.KetThuc.ToString("yyyy-MM-ddTHH:mm:ss")
                }).ToList();


            ViewBag.DanhSachSanh = dsTrangThai;
            ViewBag.LichDaDat = lichDaDat;

            var danhsachmonan = _context.MonAns.ToList();
            ViewBag.DanhSachMonAn = danhsachmonan;

            return View(new HopDongLichDat
            {
                HopDong = new HopDong
                {
                    NgayKy = DateTime.Now,
                    SoLuong = 0,
                    TienCoc = 0
                },
                LichDatSanh = new LichDatSanh
                {
                    BatDau = DateTime.Now,
                    KetThuc = DateTime.Now.AddHours(2)
                }
            });
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

        private async Task<HopDong?> LoadFullHopDong(Guid id)
        {
            return await _context.HopDongs
                .Include(h => h.ChiTietDatTiecs).ThenInclude(ct => ct.MonAn)
                .Include(h => h.LichDatSanhs).ThenInclude(l => l.Sanh)
                .FirstOrDefaultAsync(h => h.MaHD == id);
        }


        public async Task<IActionResult> updateHopDong(Guid? id)
        {
            if (id == null) return NotFound();

            var hopDong = await LoadFullHopDong(id.Value);
            if (hopDong == null) return NotFound();

            SetDropdowns(hopDong);

            // ✅ Danh sách món ăn dạng đơn giản: chỉ lấy MaMon + TenMon
            ViewBag.DanhSachMonAn = _context.MonAns
                .Select(m => new { m.MaMon, m.TenMon })
                .ToList();

            ViewBag.DanhSachSanh = _context.SanhTiecs.ToList();

            return View(hopDong);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateHopDong(HopDong hopDong)
        {
            if (!ModelState.IsValid)
            {
                SetDropdowns(hopDong);
                ViewBag.DanhSachMonAn = _context.MonAns.ToList(); // ✅ Bổ sung lại nếu có lỗi
                return View(hopDong);
            }

            var existingHopDong = await LoadFullHopDong(hopDong.MaHD);
            if (existingHopDong == null)
            {
                return NotFound();
            }

            try
            {
                // ✅ Cập nhật các trường (loại bỏ TrangThai)
                existingHopDong.NgayKy = hopDong.NgayKy;
                existingHopDong.TienCoc = hopDong.TienCoc;
                existingHopDong.MaKH = hopDong.MaKH;
                existingHopDong.MaNV = hopDong.MaNV;
                existingHopDong.MaDV = hopDong.MaDV;

                // ✅ Cập nhật ChiTietDatTiecs (thêm, xoá, sửa)
                var updatedMonIds = hopDong.ChiTietDatTiecs.Select(ct => ct.MaMon).ToList();
                var monToRemove = existingHopDong.ChiTietDatTiecs
                                                    .Where(x => !updatedMonIds.Contains(x.MaMon))
                                                    .ToList();

                foreach (var mon in monToRemove)
                {
                    existingHopDong.ChiTietDatTiecs.Remove(mon);
                }

                foreach (var ct in hopDong.ChiTietDatTiecs)
                {
                    var existingCT = existingHopDong.ChiTietDatTiecs.FirstOrDefault(x => x.MaMon == ct.MaMon);
                    if (existingCT != null)
                    {
                        existingCT.SoLuong = ct.SoLuong;
                        existingCT.TrangThai = ct.TrangThai;
                    }
                    else
                    {
                        existingHopDong.ChiTietDatTiecs.Add(new ChiTietDatTiec
                        {
                            MaHD = existingHopDong.MaHD,
                            MaMon = ct.MaMon,
                            SoLuong = ct.SoLuong,
                            TrangThai = ct.TrangThai
                        });
                    }
                }

                // ✅ Cập nhật LichDatSanhs như cũ
                foreach (var lich in hopDong.LichDatSanhs)
                {
                    var existingLich = existingHopDong.LichDatSanhs.FirstOrDefault(x => x.MaSanh == lich.MaSanh);
                    if (existingLich != null)
                    {
                        existingLich.BatDau = lich.BatDau;
                        existingLich.KetThuc = lich.KetThuc;
                        existingLich.TrangThai = lich.TrangThai;
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(getHopDong));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HopDongExists(hopDong.MaHD))
                    return NotFound();
                else
                    throw;
            }
        }


        private void SetDropdowns(HopDong hopDong)
        {
            ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "TenKH", hopDong.MaKH);
            ViewData["MaNV"] = new SelectList(_context.NhanViens, "MaNV", "ChucVu", hopDong.MaNV);
            ViewData["MaDV"] = new SelectList(_context.DichVus, "MaDV", "TenDV", hopDong.MaDV);
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
