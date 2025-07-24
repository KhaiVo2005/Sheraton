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

namespace Sheraton.Areas.HumanResources.Controllers
{
    [CheckRole("HumanResources")]
    [Area("HumanResources")]
    public class NhanViensController : Controller
    {
        private readonly SheratonDbContext _context;

        public NhanViensController(SheratonDbContext context)
        {
            _context = context;
        }

        // GET: HumanResources/NhanViens
        public async Task<IActionResult> getNhanVien()
        {
            return View(await _context.NhanViens.ToListAsync());
        }

        // GET: HumanResources/NhanViens/Details/5
        public async Task<IActionResult> detailsNhanVien(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.MaNV == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: HumanResources/NhanViens/Create
        public IActionResult createNhanVien()
        {
            return View();
        }

        // POST: HumanResources/NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> createNhanVien([Bind("MaNV,TenNV,SDT,Email,GioiTinh,ChucVu,TK,MK")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                nhanVien.MaNV = Guid.NewGuid();
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(getNhanVien));
            }
            return View(nhanVien);
        }

        // GET: HumanResources/NhanViens/Edit/5
        public async Task<IActionResult> updateNhanVien(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            return View(nhanVien);
        }

        // POST: HumanResources/NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateNhanVien(Guid id, [Bind("MaNV,TenNV,SDT,Email,GioiTinh,ChucVu")] NhanVien nhanVien)
        {
            if (id != nhanVien.MaNV)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.MaNV))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(getNhanVien));
            }
            return View(nhanVien);
        }

        // GET: HumanResources/NhanViens/Delete/5
        public async Task<IActionResult> deleteNhanVien(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .FirstOrDefaultAsync(m => m.MaNV == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: HumanResources/NhanViens/Delete/5
        [HttpPost, ActionName("deleteNhanVien")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(getNhanVien));
        }

        private bool NhanVienExists(Guid id)
        {
            return _context.NhanViens.Any(e => e.MaNV == id);
        }
    }
}
