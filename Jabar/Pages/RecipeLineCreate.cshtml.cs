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

        public async Task<IActionResult> OnGetAsync(int assemblyRecipeId)
        {
            Items = await _context.Items.ToListAsync();
            AssemblyRecipeId = assemblyRecipeId;
            ViewData["Item"] = new SelectList(_context.Items, "Item", "Item");

            ViewData["ItemName"] = new SelectList(_context.Items, "ItemId", "ItemName");
            ViewData["AssemblyRecipeId"] = new SelectList(_context.AssemblyRecipes, "AssemblyRecipeId", "AssemblyRecipeId");
            //ViewBag.Classifications = dbCC.Classifications
            //.Select(c => new SelectListItem { Value = c.ClassificationId, Text = c.Description })
            //.ToList();
            return Page();
        }

        [BindProperty]
        public RecipeLine RecipeLine { get; set; }

        public IList<Item> Items { get; set; }

        public int AssemblyRecipeId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            RecipeLine.LastModifiedBy = "AlphaTech";//change to current user
            RecipeLine.LastModifiedDate = DateTime.Today;
            RecipeLine.Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == RecipeLine.ItemId);
            RecipeLine.AssemblyRecipe = await _context.AssemblyRecipes.FirstOrDefaultAsync(m => m.AssemblyRecipeId == RecipeLine.AssemblyRecipeId);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.RecipeLines.Add(RecipeLine);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}