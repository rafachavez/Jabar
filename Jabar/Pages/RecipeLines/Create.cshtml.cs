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
        public Item Item { get; set; }

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
            if(notCircular(AssemblyRecipe, RecipeLines))//.ItemId != RecipeLine.ItemId)
            {
                //need to increase count when adding items that are already a part of the assembly recipe
                //instead of adding a new line
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
                Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == RecipeLine.ItemId);
                RecipeLine.Item = Item;
                _context.RecipeLines.Add(RecipeLine);
                await _context.SaveChangesAsync();
                return RedirectToPage("/AssemblyRecipes/Details", new { id = RecipeLine.AssemblyRecipeId });
            }
            return RedirectToPage("/AssemblyRecipes/Details", new { id = RecipeId });
        }

        private bool notCircular(AssemblyRecipe assemblyRecipe, IList<RecipeLine> recipeLines)
        {
            //stop if it's itself... working
            if(assemblyRecipe.ItemId == RecipeLine.ItemId)
            {
                return false;
            }
            
            //not working, checking for circular assemblies.
            //foreach (var line in recipeLines)
            //{
            //    Item = _context.Items.FirstOrDefault(m => m.ItemId == line.ItemId);
            //    if (Item.IsAssembled)
            //    {
            //        var lines = from i in _context.RecipeLines
            //                    where i.AssemblyRecipeId == Item.AssemblyRecipeId
            //                    select i;
            //        IList<RecipeLine> newLines = lines.ToList();

            //        AssemblyRecipe assembly = _context.AssemblyRecipes.FirstOrDefault(r => r.AssemblyRecipeId == Item.AssemblyRecipeId);
            //        return notCircular(assembly, newLines);
            //    }
            //}            

            return true;
        }
    }
}