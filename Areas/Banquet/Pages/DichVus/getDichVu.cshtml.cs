using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sheraton.Data;
using Sheraton.Models;

namespace Sheraton.Areas.Banquet.Pages.DichVus
{
    public class getDichVuModel : PageModel
    {
        private readonly SheratonDbContext _context;
        [BindProperty]
        public List<DichVu> DichVus { get; set; }
        public getDichVuModel(SheratonDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            DichVus = _context.DichVus.ToList();
        }
    }
}
