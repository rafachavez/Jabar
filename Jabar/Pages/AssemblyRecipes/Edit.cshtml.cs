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

namespace Jabar.Pages.AssemblyRecipes
{
    public class EditModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public EditModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AssemblyRecipe AssemblyRecipe { get; set; }

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
           ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AssemblyRecipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssemblyRecipeExists(AssemblyRecipe.AssemblyRecipeId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AssemblyRecipeExists(int id)
        {
            return _context.AssemblyRecipes.Any(e => e.AssemblyRecipeId == id);
        }
    }
}
