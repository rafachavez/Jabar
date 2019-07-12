using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.Items
{
    public class DetailsModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DetailsModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);

            if (Item == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
