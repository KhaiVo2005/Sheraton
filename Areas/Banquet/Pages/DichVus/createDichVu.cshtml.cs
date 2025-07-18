using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Sheraton.Data;
using Sheraton.Models;

namespace Sheraton.Areas.Banquet.Pages.DichVus
{
    public class createDichVuModel : PageModel
    {
        public SheratonDbContext _context;
        [BindProperty]
        public DichVu DichVu { get; set; }

        public createDichVuModel(SheratonDbContext context)
        {
            _context = context;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.DichVus.Add(DichVu);
            _context.SaveChanges();
            return RedirectToPage("getDichVu");
        }
    }
}
