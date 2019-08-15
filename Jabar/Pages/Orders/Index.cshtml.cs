using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jabar.Pages.Orders
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public IndexModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OrderItem> OrderItem { get;set; }

        public async Task OnGetAsync()
        {
            OrderItem = await _context.OrderItems.ToListAsync();
        }
    }
}
