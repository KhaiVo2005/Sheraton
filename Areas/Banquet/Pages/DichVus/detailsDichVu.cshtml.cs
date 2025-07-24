using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sheraton.Data;
using Sheraton.Models;
using WebTravel.Attribute;

namespace Sheraton.Areas.Banquet.Pages.DichVus
{
    [CheckRole("Banquet")]
    public class detailsDichVuModel : PageModel
    {
        private readonly SheratonDbContext _context;

        public detailsDichVuModel(SheratonDbContext context)
        {
            _context = context;
        }

        public DichVu DichVu { get; set; }

        public IActionResult OnGet(Guid id)
        {
            DichVu = _context.DichVus.FirstOrDefault(d => d.MaDV == id);
            if (DichVu == null)
                return NotFound();

            return Page();
        }
    }
}
