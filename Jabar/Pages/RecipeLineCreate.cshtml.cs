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

namespace Jabar.Pages
{
    public class RecipeLineCreateModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public RecipeLineCreateModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;            
        }

        public async Task<IActionResult> OnGetAsync(int? ItemId)
        {
            
            ViewData["ItemId"] = ItemId;
            
            Items = await _context.Items.ToListAsync();
            AssemblyRecipes = await _context.AssemblyRecipes.ToListAsync();
           // AssemblyRecipe = await _context.AssemblyRecipes.FirstOrDefaultAsync(m => m.AssemblyRecipeId == Id);
            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == ItemId);
            RecipeLine = new RecipeLine();
            generatingAssembly = (int)Id;
            //RecipeLine.AssemblyRecipeId = Id;
            //ViewData["Item"] = new SelectList(_context.Items, "Item", "Item");

            ViewData["ItemName"] = new SelectList(_context.Items, "ItemId", "ItemName");
            //ViewData["AssemblyRecipeId"] = new SelectList(_context.AssemblyRecipes, "AssemblyRecipeId", "AssemblyRecipeId");
           
            return Page();
        }

        [BindProperty]
        public RecipeLine RecipeLine { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int ItemId { get; set; }

        public AssemblyRecipe AssemblyRecipe { get; set; }

        public IList<Item> Items { get; set; }

        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; }

        

        public IList<AssemblyRecipe> AssemblyRecipes { get; set; }

        public int generatingAssembly { get; set; }


        public async Task<IActionResult> OnPostAddAsync()
        {
            
            //AssemblyRecipe = await _context.AssemblyRecipes.FirstOrDefaultAsync(m => m.AssemblyRecipeId == Id);
            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == ItemId);
            RecipeLine.LastModifiedBy = "AlphaTech";//change to current user
            RecipeLine.LastModifiedDate = DateTime.Today;
            
            //these need an item and a recipe to post to the db
            //RecipeLine.Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == RecipeLine.ItemId);//itemid is chosen by user
            //RecipeLine.AssemblyRecipe = await _context.AssemblyRecipes.FirstOrDefaultAsync(m => m.AssemblyRecipeId == RecipeLine.AssemblyRecipeId);
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //AssemblyRecipe.RecipeLines.Append(RecipeLine);
            if(Item.RecipeLines == null)
            {
                Item.RecipeLines = Enumerable.Empty<RecipeLine>();
            }
            
            Item.RecipeLines.Append(RecipeLine);


            _context.Attach(Item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(Item.ItemId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _context.RecipeLines.Add(RecipeLine);
            await _context.SaveChangesAsync();

            return RedirectToPage("AssemblyRecipeCreate", new {Id = Item.ItemId });
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}