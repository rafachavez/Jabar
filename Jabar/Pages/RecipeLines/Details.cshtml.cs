using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.RecipeLines
{
    public class DetailsModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DetailsModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public RecipeLine RecipeLine { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RecipeLine = await _context.RecipeLines
                .Include(r => r.AssemblyRecipe)
                .Include(r => r.Item).FirstOrDefaultAsync(m => m.RecipeLineId == id);

            if (RecipeLine == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
