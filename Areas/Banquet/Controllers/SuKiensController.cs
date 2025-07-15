using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Models;

namespace Sheraton.Areas.Banquet.Controllers
{
    [Area("Banquet")]
    public class SuKiensController : Controller
    {
        private readonly SheratonDbContext _context;

        public SuKiensController(SheratonDbContext context)
        {
            _context = context;
        }

        // GET: Banquet/SuKiens
        public async Task<IActionResult> Index()
        {
            var sheratonDbContext = _context.HopDongs.Include(h => h.DichVu).Include(h => h.KhachHang).Include(h => h.NhanVien);
            return View(await sheratonDbContext.ToListAsync());
        }

        // GET: Banquet/SuKiens/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["MaDV"] = new SelectList(_context.DichVus, "MaDV", "MoTa", hopDong.MaDV);
            ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "Email", hopDong.MaKH);
            ViewData["MaNV"] = new SelectList(_context.NhanViens, "MaNV", "ChucVu", hopDong.MaNV);
            return View(hopDong);
        }

        // POST: Banquet/SuKiens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MaHD,NgayKy,TienCoc,SoLuong,TrangThai,PTTT,MaKH,MaNV,MaDV")] HopDong hopDong)
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDV"] = new SelectList(_context.DichVus, "MaDV", "MoTa", hopDong.MaDV);
            ViewData["MaKH"] = new SelectList(_context.KhachHangs, "MaKH", "Email", hopDong.MaKH);
            ViewData["MaNV"] = new SelectList(_context.NhanViens, "MaNV", "ChucVu", hopDong.MaNV);
            return View(hopDong);
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
    }
}
