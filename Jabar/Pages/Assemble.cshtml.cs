using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jabar.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Jabar.Pages
{
    [Authorize] // Uncomment this when we are ready to implement Identity
    public class AssembleModel : PageModel
    {

        private readonly Jabar.Data.ApplicationDbContext _context;

        public AssembleModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int id)
        {
            var items = from i in _context.Items
                        where i.IsAssembled == true
                        select i;
            //this should now only show items that are assembled
            ViewData["ItemId"] = new SelectList(items, "ItemId", "ItemName");

            var lines = from l in _context.RecipeLines
                        where l.AssemblyRecipeId == id
                        select l;
            RecipeLines = await lines.ToListAsync();
            //return Page(AssemblyRecipe.AssemblyRecipeId);
        }

        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; }

        [BindProperty(SupportsGet = true)]
        public AssemblyRecipe AssemblyRecipe { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<RecipeLine> RecipeLines { get; set; }


        public async Task<IActionResult> OnPostAsync(int id)
        {

            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);

            var lines = from l in _context.RecipeLines
                        where l.AssemblyRecipeId == id
                        select l;
            RecipeLines = lines.ToList();

            //Item.OnHandQty++;
            //_context.Attach(Item).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ItemExists(Item.ItemId))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            return RedirectToPage("/Assemble", new { id = AssemblyRecipe.AssemblyRecipeId });
        }

    }
}