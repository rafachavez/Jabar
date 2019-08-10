using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.Items
{
    public class DeleteModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DeleteModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Item Item { get; set; }

        [BindProperty]
        public AssemblyRecipe AssemblyRecipe { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Item = await _context.Items.FindAsync(id);

            if (Item != null)
            {
                //if its an assemble item then delete its recipe first
                if (Item.IsAssembled)
                {
                    AssemblyRecipe = await _context.AssemblyRecipes.FindAsync(Item.AssemblyRecipeId);
                    _context.AssemblyRecipes.Remove(AssemblyRecipe);
                    await _context.SaveChangesAsync();
                }
                //need to handle deleting items that are part of an assembly recipe
                _context.Items.Remove(Item);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("/Inventory");
        }
    }
}
