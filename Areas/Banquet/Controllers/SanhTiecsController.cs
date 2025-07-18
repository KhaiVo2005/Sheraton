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
        public class SanhTiecsController : Controller
        {
            private readonly SheratonDbContext _context;

            public SanhTiecsController(SheratonDbContext context)
            {
                _context = context;
            }

            // GET: Banquet/SanhTiecs
            public async Task<IActionResult> getSanhTiec()
            {
                return View(await _context.SanhTiecs.ToListAsync());
            }

            // GET: Banquet/SanhTiecs/Details/5
            public async Task<IActionResult> detailsSanhTiec(Guid? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var sanhTiec = await _context.SanhTiecs
                    .FirstOrDefaultAsync(m => m.MaSanh == id);
                if (sanhTiec == null)
                {
                    return NotFound();
                }

                return View(sanhTiec);
            }

            // GET: Banquet/SanhTiecs/Create
            public IActionResult createSanhTiec()
            {
                return View();
            }

            // POST: Banquet/SanhTiecs/Create
            // To protect from overposting attacks, enable the specific properties you want to bind to.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> createSanhTiec([Bind("MaSanh,TenSanh,SucChua,MoTa,Gia,TrangThai")] SanhTiec sanhTiec, IFormFile ImageFile)
            {
                if (ModelState.IsValid)
                {
                    sanhTiec.MaSanh = Guid.NewGuid();

                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/sanh");

                        if (!Directory.Exists(uploadPath))
                            Directory.CreateDirectory(uploadPath);

                        var fullPath = Path.Combine(uploadPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        sanhTiec.HinhAnh = "/image/sanh/" + fileName;
                    }

                _context.Add(sanhTiec);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(getSanhTiec));
                }
                return View(sanhTiec);
            }

            // GET: Banquet/SanhTiecs/Edit/5
            public async Task<IActionResult> updateSanhTiec(Guid? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var sanhTiec = await _context.SanhTiecs.FindAsync(id);
                if (sanhTiec == null)
                {
                    return NotFound();
                }
                return View(sanhTiec);
            }

        // POST: Banquet/SanhTiecs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> updateSanhTiec(Guid id, [Bind("MaSanh,TenSanh,SucChua,MoTa,Gia,TrangThai")] SanhTiec sanhTiec, IFormFile ImageFile)
        {
            if (id != sanhTiec.MaSanh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingSanhTiec = await _context.SanhTiecs.AsNoTracking().FirstOrDefaultAsync(s => s.MaSanh == id);
                    if (existingSanhTiec == null)
                        return NotFound();

                    // Giữ lại đường dẫn ảnh cũ nếu không có ảnh mới
                    sanhTiec.HinhAnh = existingSanhTiec.HinhAnh;

                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/image/sanh");

                        if (!Directory.Exists(uploadPath))
                            Directory.CreateDirectory(uploadPath);

                        var fullPath = Path.Combine(uploadPath, fileName);
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(stream);
                        }

                        // Cập nhật đường dẫn ảnh mới
                        sanhTiec.HinhAnh = "/image/sanh/" + fileName;
                    }

                    _context.Update(sanhTiec);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanhTiecExists(sanhTiec.MaSanh))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(getSanhTiec));
            }
            return View(sanhTiec);
        }


        // GET: Banquet/SanhTiecs/Delete/5
        public async Task<IActionResult> deleteSanhTiec(Guid? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var sanhTiec = await _context.SanhTiecs
                    .FirstOrDefaultAsync(m => m.MaSanh == id);
                if (sanhTiec == null)
                {
                    return NotFound();
                }

                return View(sanhTiec);
            }

            // POST: Banquet/SanhTiecs/Delete/5
            [HttpPost, ActionName("deleteSanhTiec")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(Guid id)
            {
                var sanhTiec = await _context.SanhTiecs.FindAsync(id);
                if (sanhTiec != null)
                {
                    _context.SanhTiecs.Remove(sanhTiec);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(getSanhTiec));
            }

            private bool SanhTiecExists(Guid id)
            {
                return _context.SanhTiecs.Any(e => e.MaSanh == id);
            }
        }
    }
