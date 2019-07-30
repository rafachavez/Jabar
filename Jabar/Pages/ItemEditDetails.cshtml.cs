using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages
{
    public class ItemEditDetailsModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public ItemEditDetailsModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
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

        public async Task<IActionResult> OnPostAsync()
        {
            Item.LastModifiedBy = "AlphaTech";//change to current user
            Item.LastModifiedDate = DateTime.Today;
       
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            
            _context.Attach(Item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.ItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Inventory");
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}
