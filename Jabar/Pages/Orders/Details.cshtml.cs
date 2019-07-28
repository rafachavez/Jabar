using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DetailsModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public OrderItem OrderItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderItem = await _context.OrderItems.FirstOrDefaultAsync(m => m.OrderItemId == id);

            if (OrderItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
