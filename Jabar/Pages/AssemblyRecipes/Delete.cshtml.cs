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
    public class DeleteModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DeleteModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AssemblyRecipe AssemblyRecipe { get; set; }

        [BindProperty]
        public Item Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssemblyRecipe = await _context.AssemblyRecipes
                .Include(a => a.Item).FirstOrDefaultAsync(m => m.AssemblyRecipeId == id);

            if (AssemblyRecipe == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AssemblyRecipe = await _context.AssemblyRecipes.FindAsync(id);
            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == AssemblyRecipe.ItemId);
            if (AssemblyRecipe != null)
            {
                Item.AssemblyRecipeId = null;
                Item.IsAssembled = false;
                Item.LastModifiedBy = "AlphaTech"; //change to user
                Item.LastModifiedDate = DateTime.Today;
                _context.AssemblyRecipes.Remove(AssemblyRecipe);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Items/Details", id);
        }
    }
}
