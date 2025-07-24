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

namespace Sheraton.Areas.Sale.Controllers
{
    [CheckRole("Sale")]
    [Area("Sale")]
    public class KhachHangsController : Controller
    {
        private readonly SheratonDbContext _context;

        public KhachHangsController(SheratonDbContext context)
        {
            _context = context;
        }

        // GET: Sale/KhachHangs
        public async Task<IActionResult> getKhachHang()
        {
            return View(await _context.KhachHangs.ToListAsync());
        }

        // GET: Sale/KhachHangs/Details/5
        public async Task<IActionResult> detailsKhachHang(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKH == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // GET: Sale/KhachHangs/Create
        public IActionResult createKhachHang()
        {
            return View();
        }

        // POST: Sale/KhachHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> createKhachHang([Bind("MaKH,TenKH,SDT,GioiTinh,Email")] KhachHangg khachHang)
        {
            if (ModelState.IsValid)
            {
                khachHang.MaKH = Guid.NewGuid();
                _context.Add(khachHang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(getKhachHang));
            }
            return View(khachHang);
        }

        // GET: Sale/KhachHangs/Edit/5
        public async Task<IActionResult> updateKhachHang(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: Sale/KhachHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateKhachHang(Guid id, [Bind("MaKH,TenKH,SDT,GioiTinh,Email")] KhachHangg khachHang)
        {
            if (id != khachHang.MaKH)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.MaKH))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(getKhachHang));
            }
            return View(khachHang);
        }

        // GET: Sale/KhachHangs/Delete/5
        public async Task<IActionResult> deleteKhachHang(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs
                .FirstOrDefaultAsync(m => m.MaKH == id);
            if (khachHang == null)
            {
                return NotFound();
            }

            return View(khachHang);
        }

        // POST: Sale/KhachHangs/Delete/5
        [HttpPost, ActionName("deleteKhachHang")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang != null)
            {
                _context.KhachHangs.Remove(khachHang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(getKhachHang));
        }

        private bool KhachHangExists(Guid id)
        {
            return _context.KhachHangs.Any(e => e.MaKH == id);
        }
    }
}
