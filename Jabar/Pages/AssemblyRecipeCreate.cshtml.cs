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

namespace Jabar.Pages
{
    public class AssemblyRecipeCreateModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public AssemblyRecipeCreateModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);

            if (Item == null)
            {
                return NotFound();
            }
            Items = await _context.Items.ToListAsync();
            AssemblyRecipe = await _context.AssemblyRecipes.FirstOrDefaultAsync(m => m.AssemblyRecipeId == Item.AssemblyRecipeId);
            //RecipeLines = AssemblyRecipe.RecipeLines.ToList();
            RecipeLine = new RecipeLine();
            ViewData["ItemName"] = new SelectList(_context.Items, "ItemId", "ItemName");
            ViewData["AssemblyRecipeId"] = new SelectList(_context.AssemblyRecipes, "AssemblyRecipeId", "AssemblyRecipeId");
            return Page();
        }

       

        [BindProperty(SupportsGet = true)]
        public AssemblyRecipe AssemblyRecipe { get; set; }

        [BindProperty(SupportsGet = true)]
        public int id { get; set; }

        [BindProperty(SupportsGet = true)]
        public RecipeLine RecipeLine { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<Item> Items { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<RecipeLine> RecipeLines { get; set; }

        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; }

        //used by recipeline create modal
        public async Task<IActionResult> OnPostCreateLineAsync(int ItemToAdd, int? id)
        {

            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);
            RecipeLine.LastModifiedBy = "AlphaTech";//change this to whoever is logged in
            RecipeLine.LastModifiedDate = DateTime.Today;//this is probably good
            //RecipeLine.AssemblyRecipeId = AssemblyRecipe.AssemblyRecipeId;
           


            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Item.RecipeLines == null)
            {
                Item.RecipeLines = Enumerable.Empty<RecipeLine>();
            }

            Item.RecipeLines.Append(RecipeLine);
           
            RecipeLines.Add(RecipeLine);

            //_context.RecipeLines.Add(RecipeLine);
            _context.Attach(Item).State = EntityState.Modified;
            //_context.Attach(AssemblyRecipe).State = EntityState.Modified;
            //_context.Attach(RecipeLine).State = EntityState.Modified;
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
           
            await _context.SaveChangesAsync();
            

            return RedirectToPage();
        }


        //this is the one for saving the whole recipe
        public async Task<IActionResult> OnPostAsync()
        {
           
            //should check for circular logic here too.
            //Im getting this far!!!
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AssemblyRecipes.Add(AssemblyRecipe);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }
    }
}