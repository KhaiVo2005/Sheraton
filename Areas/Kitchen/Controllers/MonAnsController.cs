using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Models;

namespace Sheraton.Areas.Kitchen.Controllers
{
    [Area("Kitchen")]
    public class MonAnsController : Controller
    {
        private readonly SheratonDbContext _context;

        public MonAnsController(SheratonDbContext context)
        {
            _context = context;
        }

        // GET: Kitchen/MonAns
        public async Task<IActionResult> getMonAn()
        {
            return View(await _context.MonAns.ToListAsync());
        }

        // GET: Kitchen/MonAns/Details/5
        public async Task<IActionResult> detailsMonAn(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monAn = await _context.MonAns
                .FirstOrDefaultAsync(m => m.MaMon == id);
            if (monAn == null)
            {
                return NotFound();
            }

            return View(monAn);
        }

        // GET: Kitchen/MonAns/Create
        public IActionResult createMonAn()
        {
            return View();
        }

        // POST: Kitchen/MonAns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> createMonAn([Bind("MaMon,TenMon,DonGia,MoTa")] MonAn monAn)
        {
            if (ModelState.IsValid)
            {
                monAn.MaMon = Guid.NewGuid();
                _context.Add(monAn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(getMonAn));
            }
            return View(monAn);
        }

        // GET: Kitchen/MonAns/Edit/5
        public async Task<IActionResult> updateMonAn(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monAn = await _context.MonAns.FindAsync(id);
            if (monAn == null)
            {
                return NotFound();
            }
            return View(monAn);
        }

        // POST: Kitchen/MonAns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateMonAn(Guid id, [Bind("MaMon,TenMon,DonGia,MoTa")] MonAn monAn)
        {
            if (id != monAn.MaMon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monAn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonAnExists(monAn.MaMon))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(getMonAn));
            }
            return View(monAn);
        }

        // GET: Kitchen/MonAns/Delete/5
        public async Task<IActionResult> deleteMonAn(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monAn = await _context.MonAns
                .FirstOrDefaultAsync(m => m.MaMon == id);
            if (monAn == null)
            {
                return NotFound();
            }

            return View(monAn);
        }

        // POST: Kitchen/MonAns/Delete/5
        [HttpPost, ActionName("deleteMonAn")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var monAn = await _context.MonAns.FindAsync(id);
            if (monAn != null)
            {
                _context.MonAns.Remove(monAn);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(getMonAn));
        }

        private bool MonAnExists(Guid id)
        {
            return _context.MonAns.Any(e => e.MaMon == id);
        }
    }
}
