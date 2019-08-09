using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Jabar.Data;
using Jabar.Models;
using Microsoft.EntityFrameworkCore;

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

        [BindProperty(SupportsGet = true)]
        public AssemblyRecipe AssemblyRecipe { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<RecipeLine> RecipeLines { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            AssemblyRecipe = await _context.AssemblyRecipes
                .Include(a => a.Item).FirstOrDefaultAsync(m => m.AssemblyRecipeId == RecipeId);

            var lines = from i in _context.RecipeLines
                        where i.AssemblyRecipeId == RecipeId
                        select i;
            RecipeLines = await lines.ToListAsync();
            //need to make sure its not adding itself
            if(AssemblyRecipe.ItemId != RecipeLine.ItemId)
            {
                //need to increase count when adding items that are already a part of the assembly recipe
                foreach (var line in RecipeLines)
                {
                    if (RecipeLine.ItemId == line.ItemId)
                    {
                        line.RequiredItemQty += RecipeLine.RequiredItemQty;
                        _context.RecipeLines.Update(line);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("/AssemblyRecipes/Details", new { id = RecipeId });
                    }
                }
                RecipeLine.AssemblyRecipeId = RecipeId;
                RecipeLine.LastModifiedBy = "AlphaTech";//set to logged in user
                RecipeLine.LastModifiedDate = DateTime.Today;
                _context.RecipeLines.Add(RecipeLine);
                await _context.SaveChangesAsync();
                return RedirectToPage("/AssemblyRecipes/Details", new { id = RecipeLine.AssemblyRecipeId });
            }
            return RedirectToPage("/AssemblyRecipes/Details", new { id = RecipeId });
        }
    }
}