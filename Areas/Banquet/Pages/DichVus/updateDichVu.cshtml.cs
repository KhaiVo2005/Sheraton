using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sheraton.Data;
using Sheraton.Models;
using WebTravel.Attribute;

namespace Sheraton.Areas.Banquet.Pages.DichVus
{
    [CheckRole("Banquet")]
    public class updateDichVuModel : PageModel
    {
        private readonly SheratonDbContext _context;

        public updateDichVuModel(SheratonDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public DichVu DichVu { get; set; }

        // GET: load dữ liệu cũ
        public IActionResult OnGet(Guid id)
        {
            DichVu = _context.DichVus.FirstOrDefault(d => d.MaDV == id);
            if (DichVu == null)
                return NotFound();

            return Page();
        }

        // POST: lưu dữ liệu chỉnh sửa
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            var dichVuToUpdate = _context.DichVus.FirstOrDefault(d => d.MaDV == DichVu.MaDV);
            if (dichVuToUpdate == null)
                return NotFound();

            dichVuToUpdate.TenDV = DichVu.TenDV;
            dichVuToUpdate.DonGia = DichVu.DonGia;
            dichVuToUpdate.MoTa = DichVu.MoTa;

            _context.SaveChanges();

            return RedirectToPage("getDichVu");
        }
    }
}
