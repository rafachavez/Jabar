using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.AssemblyRecipes
{
    public class DetailsModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DetailsModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public AssemblyRecipe AssemblyRecipe { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<RecipeLine> RecipeLines { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<Item> Items { get; set; }

        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; }

        [BindProperty(SupportsGet = true)]
        public int itemId { get; set; }//this is populated by the hidden input

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
            //populate assembly lines

            var lines = from i in _context.RecipeLines
                        where i.AssemblyRecipeId == id
                        select i;
            RecipeLines = await lines.ToListAsync();


            Items = await _context.Items.ToListAsync();
            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == itemId);

            return Page();
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
                _context.Attach(Item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
           // return RedirectToPage("AssemblyRecipes/Details", new { id });
        }


    }
}
