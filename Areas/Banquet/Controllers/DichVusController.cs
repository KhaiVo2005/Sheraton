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
    public class DichVusController : Controller
    {
        private readonly SheratonDbContext _context;

        public DichVusController(SheratonDbContext context)
        {
            _context = context;
        }

        // GET: Banquet/DichVus
        public async Task<IActionResult> getDichVu()
        {
            return View(await _context.DichVus.ToListAsync());
        }

        // GET: Banquet/DichVus/Details/5
        public async Task<IActionResult> detailsDichVu(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus
                .FirstOrDefaultAsync(m => m.MaDV == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }

        // GET: Banquet/DichVus/Create
        public IActionResult createDichVu()
        {
            return View();
        }

        // POST: Banquet/DichVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> createDichVu([Bind("MaDV,TenDV,DonGia,MoTa")] DichVu dichVu)
        {
            if (ModelState.IsValid)
            {
                dichVu.MaDV = Guid.NewGuid();
                _context.Add(dichVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(getDichVu));
            }
            return View(dichVu);
        }

        // GET: Banquet/DichVus/Edit/5
        public async Task<IActionResult> updateDichVu(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }
            return View(dichVu);
        }

        // POST: Banquet/DichVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateDichVu(Guid id, [Bind("MaDV,TenDV,DonGia,MoTa")] DichVu dichVu)
        {
            if (id != dichVu.MaDV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dichVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DichVuExists(dichVu.MaDV))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(getDichVu));
            }
            return View(dichVu);
        }

        // GET: Banquet/DichVus/Delete/5
        public async Task<IActionResult> deleteDichVu(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dichVu = await _context.DichVus
                .FirstOrDefaultAsync(m => m.MaDV == id);
            if (dichVu == null)
            {
                return NotFound();
            }

            return View(dichVu);
        }

        // POST: Banquet/DichVus/Delete/5
        [HttpPost, ActionName("deleteDichVu")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dichVu = await _context.DichVus.FindAsync(id);
            if (dichVu != null)
            {
                _context.DichVus.Remove(dichVu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(getDichVu));
        }

        private bool DichVuExists(Guid id)
        {
            return _context.DichVus.Any(e => e.MaDV == id);
        }
    }
}
