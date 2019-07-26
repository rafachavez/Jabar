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
        [BindProperty]
        public int Index { get; set; }

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

        /// <summary>
        /// This is the method called by asp-page-handler="Subtract"
        /// it gets id from asp-route-id="some int"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostSubtractAsync(int id)
        {
            //got to get all the items.... maybe a better developer doesnt but I did
            Items = await _context.Items.ToListAsync();
            //get the exact item that is being interacted with
            //the id passed in isnt 0 based so you have to subtract 1
            Item = Items[id - 1];
            //subtract one from it
            Item.OnHandQty--;
            if(Item.OnHandQty < 0)
            {
                Item.OnHandQty = 0;//no negative inventory
            }
            //get db stuff, I copied the rest of this code from the scaffolding
            //so I can only follow it, not really create it yet
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
            //stay on current page
            return RedirectToPage();
        }

        //used by createItemModal
        public async Task<IActionResult> OnPostCreateAsync()
        {

            Item.LastModifiedDate = DateTime.Today;
            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }

            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public bool SetIndex(int newIndex)
        {
            Index = newIndex;
            return true;
        }
        //used by getDetailsModal
   
      

        public async Task<IActionResult> OnGetDetailsAsync(int? id)
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
            Index = Items.IndexOf(Item);
            return Page();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

    }
}