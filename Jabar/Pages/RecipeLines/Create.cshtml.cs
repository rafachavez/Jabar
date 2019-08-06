using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.RecipeLines
{
    public class CreateModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public CreateModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AssemblyRecipeId"] = new SelectList(_context.AssemblyRecipes, "AssemblyRecipeId", "AssemblyRecipeId");
        ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            return Page();
        }

        [BindProperty]
        public RecipeLine RecipeLine { get; set; }

        [BindProperty(SupportsGet =true)]
        public int RecipeId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            RecipeLine.AssemblyRecipeId = RecipeId;
            RecipeLine.LastModifiedBy = "AlphaTech";
            RecipeLine.LastModifiedDate = DateTime.Today;
            _context.RecipeLines.Add(RecipeLine);
            await _context.SaveChangesAsync();

            return RedirectToPage("/AssemblyRecipes/Details", new { id = RecipeLine.AssemblyRecipeId });
        }
    }
}