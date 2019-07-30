using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.AssemblyRecipes
{
    public class IndexModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public IndexModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<AssemblyRecipe> AssemblyRecipe { get;set; }

        public async Task OnGetAsync()
        {
            AssemblyRecipe = await _context.AssemblyRecipes
                .Include(a => a.Item).ToListAsync();
        }
    }
}
