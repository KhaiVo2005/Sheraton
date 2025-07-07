using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Models;

namespace Sheraton.Areas.Sale.Controllers
{
    [Area("Sale")]
    public class ChiTietDatTiecsController : Controller
    {
        private readonly SheratonDbContext _context;

        public ChiTietDatTiecsController(SheratonDbContext context)
        {
            _context = context;
        }

        // GET: Sale/ChiTietDatTiecs
        public async Task<IActionResult> Index()
        {
            var sheratonDbContext = _context.ChiTietDatTiecs.Include(c => c.HopDong).Include(c => c.MonAn);
            return View(await sheratonDbContext.ToListAsync());
        }

        // GET: Sale/ChiTietDatTiecs/Details/5
        public async Task<IActionResult> getChiTietDatTiec(Guid? id)
        {
            var chiTietList = await _context.ChiTietDatTiecs
                .Where(c => c.MaHD == id)
                .Include(c => c.MonAn)
                .ToListAsync();

            if (chiTietList.Any())
            {
                return View("getChiTietDatTiec", chiTietList);
            }
            else
            {
                return RedirectToAction("Create", new { maHD = id });
            }
        }

        // GET: Sale/ChiTietDatTiecs/Create
        public IActionResult Create(Guid maHD)
        {
            var model = new ChiTietDatTiec { MaHD = maHD };

            ViewData["MaMon"] = new SelectList(_context.MonAns, "MaMon", "TenMon");
            return View(model);
        }

        // POST: Sale/ChiTietDatTiecs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SoLuong,TrangThai,MaMon,MaHD")] ChiTietDatTiec chiTietDatTiec)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietDatTiec);
                await _context.SaveChangesAsync();

                // Cho phép thêm tiếp
                return RedirectToAction("Create", new { maHD = chiTietDatTiec.MaHD });
            }

            ViewData["MaMon"] = new SelectList(_context.MonAns, "MaMon", "TenMon", chiTietDatTiec.MaMon);
            return View(chiTietDatTiec);
        }

        // GET: Sale/ChiTietDatTiecs/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietDatTiec = await _context.ChiTietDatTiecs.FindAsync(id);
            if (chiTietDatTiec == null)
            {
                return NotFound();
            }
            ViewData["MaHD"] = new SelectList(_context.HopDongs, "MaHD", "MaHD", chiTietDatTiec.MaHD);
            ViewData["MaMon"] = new SelectList(_context.MonAns, "MaMon", "MoTa", chiTietDatTiec.MaMon);
            return View(chiTietDatTiec);
        }

        // POST: Sale/ChiTietDatTiecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("SoLuong,TrangThai,MaMon,MaHD")] ChiTietDatTiec chiTietDatTiec)
        {
            if (id != chiTietDatTiec.MaHD)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chiTietDatTiec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChiTietDatTiecExists(chiTietDatTiec.MaHD))
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
            ViewData["MaHD"] = new SelectList(_context.HopDongs, "MaHD", "MaHD", chiTietDatTiec.MaHD);
            ViewData["MaMon"] = new SelectList(_context.MonAns, "MaMon", "MoTa", chiTietDatTiec.MaMon);
            return View(chiTietDatTiec);
        }

        // GET: Sale/ChiTietDatTiecs/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chiTietDatTiec = await _context.ChiTietDatTiecs
                .Include(c => c.HopDong)
                .Include(c => c.MonAn)
                .FirstOrDefaultAsync(m => m.MaHD == id);
            if (chiTietDatTiec == null)
            {
                return NotFound();
            }

            return View(chiTietDatTiec);
        }

        // POST: Sale/ChiTietDatTiecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var chiTietDatTiec = await _context.ChiTietDatTiecs.FindAsync(id);
            if (chiTietDatTiec != null)
            {
                _context.ChiTietDatTiecs.Remove(chiTietDatTiec);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChiTietDatTiecExists(Guid id)
        {
            return _context.ChiTietDatTiecs.Any(e => e.MaHD == id);
        }
    }
}
