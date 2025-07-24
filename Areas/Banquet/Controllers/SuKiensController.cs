using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebTravel.Attribute;

namespace Sheraton.Areas.Banquet.Controllers
{
    [CheckRole("Banquet")]
    [Area("Banquet")]
    public class SuKiensController : Controller
    {
        private readonly SheratonDbContext _context;

        public SuKiensController(SheratonDbContext context)
        {
            _context = context;
        }

        // GET: Banquet/SuKiens
        public async Task<IActionResult> getSuKien()
        {
            var sheratonDbContext = _context.HopDongs.Include(h => h.DichVu).Include(h => h.KhachHang).Include(h => h.NhanVien);
            return View(await sheratonDbContext.ToListAsync());
        }

        // GET: Banquet/SuKiens/Details/5
        public async Task<IActionResult> detailsSuKien(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                        .Include(h => h.DichVu)
                        .Include(h => h.KhachHang)
                        .Include(h => h.NhanVien)
                        .Include(h => h.Ratings)
                        .Include(h => h.ChiTietDatTiecs) // Thêm chi tiết
                            .ThenInclude(c => c.MonAn) // Để lấy thông tin món ăn
                        .FirstOrDefaultAsync(m => m.MaHD == id);

            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // GET: Banquet/SuKiens/Create
        public IActionResult Create()
        {
            ViewData["MaDV"] = new SelectList(_context.DichVus, "MaDV", "MoTa");
            ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "Email");
            ViewData["MaNV"] = new SelectList(_context.NhanViens, "MaNV", "ChucVu");
            return View();
        }

        // POST: Banquet/SuKiens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaHD,NgayKy,TienCoc,SoLuong,TrangThai,PTTT,MaKH,MaNV,MaDV")] HopDong hopDong)
        {
            if (ModelState.IsValid)
            {
                hopDong.MaHD = Guid.NewGuid();
                _context.Add(hopDong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDV"] = new SelectList(_context.DichVus, "MaDV", "MoTa", hopDong.MaDV);
            ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "Email", hopDong.MaKH);
            ViewData["MaNV"] = new SelectList(_context.NhanViens, "MaNV", "ChucVu", hopDong.MaNV);
            return View(hopDong);
        }

        // GET: Banquet/SuKiens/Edit/5
        [HttpGet]
        public async Task<IActionResult> updateSuKien(Guid? id)
        {
            if (id == null)
                return NotFound();

            var hopDong = await _context.HopDongs
                .Include(h => h.ChiTietDatTiecs)
                .FirstOrDefaultAsync(h => h.MaHD == id);

            if (hopDong == null)
                return NotFound();

            // Danh sách món ăn để render dropdown
            ViewBag.DanhSachMonAn = await _context.MonAns.ToListAsync();

            return View(hopDong); // View: updateSuKien.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateSuKien(Guid id, List<ChiTietDatTiec> ChiTietDatTiec)
        {
            var hopDong = await _context.HopDongs
                .Include(h => h.ChiTietDatTiecs)
                .FirstOrDefaultAsync(h => h.MaHD == id);

            if (hopDong == null)
                return NotFound();

            // Danh sách món cũ hiện có trong DB
            var oldChiTietList = hopDong.ChiTietDatTiecs.ToList();

            // Xoá món không còn trong danh sách mới
            foreach (var old in oldChiTietList)
            {
                if (!ChiTietDatTiec.Any(c => c.MaMon == old.MaMon))
                {
                    _context.ChiTietDatTiecs.Remove(old);
                }
            }

            // Thêm mới hoặc cập nhật
            foreach (var ct in ChiTietDatTiec)
            {
                // Tránh lỗi NULL: đảm bảo SoLuong có giá trị hợp lệ
                if (ct.MaMon == null || ct.SoLuong <= 0)
                    continue;

                // Normalize TrangThai
                string trangThai = ct.TrangThai?.ToLower() == "true" ? "Đã phục vụ" : "Chưa phục vụ";

                var existing = hopDong.ChiTietDatTiecs.FirstOrDefault(x => x.MaMon == ct.MaMon);
                if (existing != null)
                {
                    // Cập nhật
                    existing.SoLuong = ct.SoLuong;
                    existing.TrangThai = trangThai;
                }
                else
                {
                    // Thêm mới
                    var newCT = new ChiTietDatTiec
                    {
                        MaHD = id,
                        MaMon = ct.MaMon,
                        SoLuong = ct.SoLuong,
                        TrangThai = trangThai
                    };
                    _context.ChiTietDatTiecs.Add(newCT);
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("getSuKien");
        }


        // GET: Banquet/SuKiens/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDongs
                .Include(h => h.DichVu)
                .Include(h => h.KhachHang)
                .Include(h => h.NhanVien)
                .FirstOrDefaultAsync(m => m.MaHD == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // POST: Banquet/SuKiens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var hopDong = await _context.HopDongs.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDongs.Remove(hopDong);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HopDongExists(Guid id)
        {
            return _context.HopDongs.Any(e => e.MaHD == id);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteMonAn(Guid maHD, Guid maMon)
        {
            var chiTiet = await _context.ChiTietDatTiecs
                .FirstOrDefaultAsync(c => c.MaHD == maHD && c.MaMon == maMon);

            if (chiTiet != null)
            {
                _context.ChiTietDatTiecs.Remove(chiTiet);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("updateSuKien", new { id = maHD });
        }

    }
}
