using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sheraton.Data;
using Sheraton.Models;

namespace Sheraton.Areas.Banquet.Pages.DichVus
{
    public class deleteDichVuModel : PageModel
    {
        private readonly SheratonDbContext _context;

        public deleteDichVuModel(SheratonDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DichVu DichVu { get; set; }

        // GET: hiển thị trang xác nhận
        public IActionResult OnGet(Guid id)
        {
            DichVu = _context.DichVus.FirstOrDefault(d => d.MaDV == id);
            if (DichVu == null)
                return NotFound();

            return Page();
        }

        // POST: xác nhận xoá
        public IActionResult OnPost()
        {
            var dichVu = _context.DichVus.Find(DichVu.MaDV);
            if (dichVu == null)
                return NotFound();

            _context.DichVus.Remove(dichVu);
            _context.SaveChanges();
            return RedirectToPage("getDichVu");
        }
    }
}
