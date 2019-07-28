using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public CreateModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        
        public Vendor VendorsModel { get; set; }
        public List<SelectListItem> VendorsModelId { get; set; }

        public IActionResult OnGet()
        {
            VendorsModelId = _context.Vendors.Select(x => new SelectListItem
            {
                Value = x.VendorId.ToString(),
                Text = x.VendorName
            })
                .ToList();
            return Page();
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderItems.Add(OrderItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}