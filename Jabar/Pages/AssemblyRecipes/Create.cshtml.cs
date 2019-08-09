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

namespace Jabar.Pages.AssemblyRecipes
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
        ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
            return Page();
        }

        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; }

        [BindProperty(SupportsGet =true)]
        public AssemblyRecipe AssemblyRecipe { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == AssemblyRecipe.ItemId);
            Item.IsAssembled = true;
            
            Item.LastModifiedBy = "AlphaTech"; //change to user
            Item.LastModifiedDate = DateTime.Today;
            _context.AssemblyRecipes.Add(AssemblyRecipe);
            await _context.SaveChangesAsync();
            Item.AssemblyRecipeId = AssemblyRecipe.AssemblyRecipeId;//this isnt set until after the context is saved
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}