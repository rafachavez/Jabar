using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.Vendors
{
    public class DeleteModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DeleteModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vendor Vendor { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vendor = await _context.Vendors.FirstOrDefaultAsync(m => m.VendorId == id);

            if (Vendor == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vendor = await _context.Vendors.FindAsync(id);

            if (Vendor != null)
            {
                _context.Vendors.Remove(Vendor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
