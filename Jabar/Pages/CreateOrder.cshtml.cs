using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Jabar.Models;

namespace Jabar.Pages
{
    public class CreateOrderModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public CreateOrderModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public OrderItem Order { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderItems.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}