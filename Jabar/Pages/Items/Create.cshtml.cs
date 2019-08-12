using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.Items
{
    public class CreateModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public CreateModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "VendorName");
            return Page();
        }

        [BindProperty]
        public Item Item { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //stop from adding the same item twice
            Item temp = _context.Items.FirstOrDefault(i => i.ItemName == Item.ItemName);

            if (temp != null)
            {
                return RedirectToPage("/Inventory");
            }

            Item.LastModifiedBy = "AlphaTech";//TODO: change this to the logged in user
            Item.LastModifiedDate = DateTime.Today;
            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Inventory");
        }
    }
}