using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.RecipeLines
{
    public class EditModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public EditModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet =true)]
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
           ViewData["AssemblyRecipeId"] = new SelectList(_context.AssemblyRecipes, "AssemblyRecipeId", "AssemblyRecipeId");
           ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            RecipeLine.LastModifiedBy = "AlphTech"; //change to user
            RecipeLine.LastModifiedDate = DateTime.Today;

            _context.Attach(RecipeLine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeLineExists(RecipeLine.RecipeLineId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/AssemblyRecipes/Details", new { id = RecipeLine.AssemblyRecipeId });
        }

        private bool RecipeLineExists(int id)
        {
            return _context.RecipeLines.Any(e => e.RecipeLineId == id);
        }
    }
}
