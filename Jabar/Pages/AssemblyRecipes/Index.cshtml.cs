using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;
using Microsoft.AspNetCore.Authorization;

namespace Jabar.Pages.AssemblyRecipes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public IndexModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public IList<AssemblyRecipe> AssemblyRecipe { get;set; }

        [BindProperty(SupportsGet = true)]
        public int itemId { get; set; }

        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<RecipeLine> RecipeLines { get; set; }

        public async Task OnGetAsync()
        {
            AssemblyRecipe = await _context.AssemblyRecipes
                .Include(a => a.Item).ToListAsync();
        }

        public async Task<IActionResult> OnPostAssembleAsync(int id)
        {

            var lines = from i in _context.RecipeLines
                        where i.AssemblyRecipeId == id
                        select i;
            RecipeLines = await lines.ToListAsync();

            foreach (var line in RecipeLines)
            {
                Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == line.ItemId);
                Item.OnHandQty -= line.RequiredItemQty;
                Item.LastModifiedBy = "AlphaTech"; // change to user
                Item.LastModifiedDate = DateTime.Today;
                _context.Attach(Item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == itemId);
            Item.OnHandQty++;
            Item.LastModifiedBy = "AlphaTech"; // change to user
            Item.LastModifiedDate = DateTime.Today;
            _context.Attach(Item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage();
            // return RedirectToPage("AssemblyRecipes/Details", new { id });
        }

        public async Task<IActionResult> OnPostDisAssembleAsync(int id)
        {

            var lines = from i in _context.RecipeLines
                        where i.AssemblyRecipeId == id
                        select i;
            RecipeLines = await lines.ToListAsync();

            foreach (var line in RecipeLines)
            {
                Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == line.ItemId);
                Item.OnHandQty += line.RequiredItemQty;
                Item.LastModifiedBy = "AlphaTech"; // change to user
                Item.LastModifiedDate = DateTime.Today;
                _context.Attach(Item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == itemId);
            Item.OnHandQty--;
            Item.LastModifiedBy = "AlphaTech"; // change to user
            Item.LastModifiedDate = DateTime.Today;
            _context.Attach(Item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToPage();
            // return RedirectToPage("AssemblyRecipes/Details", new { id });
        }

    }
}
