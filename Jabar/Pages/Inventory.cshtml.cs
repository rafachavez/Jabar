using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jabar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Jabar.Pages
{
    // [Authorize] // Uncomment this when we are ready to implement Identity
    public class InventoryModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public InventoryModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Item> Items { get; set; }
        [BindProperty]
        public Item Item { get; set; }
        //[BindProperty]
        //public int Index { get; set; }

        public async Task OnGetAsync()
        {
            Items = await _context.Items.ToListAsync();
        }

        //////////////////////////////////////////////


        public async Task<IActionResult> OnPostAddAsync(int id)
        {
            Items = await _context.Items.ToListAsync();
            Item = Items[id-1];
            Item.OnHandQty++;
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
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostSubtractAsync(int id)
        {
            Items = await _context.Items.ToListAsync();
            Item = Items[id - 1];
            Item.OnHandQty--;
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
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAsync(int index)
        {
            Item = Items[index];
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

            return RedirectToPage("./Index");
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

    }
}