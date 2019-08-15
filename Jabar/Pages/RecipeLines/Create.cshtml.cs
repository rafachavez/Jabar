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
using System.Collections;

namespace Jabar.Pages.RecipeLines
{
    public class CreateModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public CreateModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGet()
        {
            ViewData["AssemblyRecipeId"] = new SelectList(_context.AssemblyRecipes, "AssemblyRecipeId", "AssemblyRecipeId");

            //these next 4 statements prevent the list of available items for an assembly from containing itself
            AssemblyRecipe = _context.AssemblyRecipes
                .Include(a => a.Item).FirstOrDefault(m => m.AssemblyRecipeId == RecipeId);

            var items = from i in _context.Items
                        where i.ItemId != AssemblyRecipe.ItemId
                        select i;
            items = items.OrderBy(s => s.ItemName);
            IList<Item> myItems = await items.ToListAsync();   

            ViewData["ItemId"] = new SelectList(myItems, "ItemId", "ItemName");
        }

        

        [BindProperty(SupportsGet = true)]
        public RecipeLine RecipeLine { get; set; }

        [BindProperty(SupportsGet = true)]
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

            IList<RecipeLine> recipeLines = lines.ToList();

            //I need to make sure Im also passing the item that is trying to be added to the 
            //assembly into notCircular
            recipeLines.Insert(0, RecipeLine);

            //need to make sure its not adding itself
            if (notCircular(recipeLines))//.ItemId != RecipeLine.ItemId)
            {
                //need to increase count when adding items that are 
                //already a part of the assembly recipe
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

        /// <summary>
        /// This is a recursive method to stop an assembly from adding an assembly that contains the first assembly
        /// </summary>
        /// <param name="assemblyRecipe"></param>
        /// <param name="recipeLines"></param>
        /// <returns></returns>
        private bool notCircular(IList<RecipeLine> recipeLines)
        {
            //check loops...WORKING
            foreach (var line in recipeLines)
            {
                //omg it finally works correctly!
                if (line.ItemId == AssemblyRecipe.ItemId)
                {
                    return false;
                }
                Item myItem = _context.Items.FirstOrDefault(m => m.ItemId == line.ItemId);
                AssemblyRecipe assembly = _context.AssemblyRecipes.FirstOrDefault(r => r.AssemblyRecipeId == myItem.AssemblyRecipeId);

                if (myItem.IsAssembled)
                {
                    var lines = from i in _context.RecipeLines
                                where i.AssemblyRecipeId == myItem.AssemblyRecipeId
                                select i;
                    IList<RecipeLine> newLines = lines.ToList();
                    //RECURSION!!!!
                    return notCircular(newLines);
                }
            }
            return true;
        }
    }
}