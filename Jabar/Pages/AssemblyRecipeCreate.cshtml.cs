using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages
{
    public class AssemblyRecipeCreateModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public AssemblyRecipeCreateModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AssemblyRecipe AssemblyRecipe { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AssemblyRecipes.Add(AssemblyRecipe);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}