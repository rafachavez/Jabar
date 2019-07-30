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
    public class IndexModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public IndexModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<RecipeLine> RecipeLine { get;set; }

        public async Task OnGetAsync()
        {
            RecipeLine = await _context.RecipeLines
                .Include(r => r.AssemblyRecipe)
                .Include(r => r.Item).ToListAsync();
        }
    }
}
