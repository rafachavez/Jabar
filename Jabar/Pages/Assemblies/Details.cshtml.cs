using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.Assemblies
{
    public class DetailsModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DetailsModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Assembly Assembly { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assembly = await _context.Assemblies.FirstOrDefaultAsync(m => m.AssemblyId == id);

            if (Assembly == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
