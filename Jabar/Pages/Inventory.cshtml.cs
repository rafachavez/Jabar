using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jabar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Jabar.Pages
{
    // [Authorize] // Uncomment this when we are ready to implement Identity
    public class InventoryModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public InventoryModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public string NameSort { get; set; }
        public string QtySort { get; set; }
        public string VendorSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int Count { get; set; }
        [BindProperty(SupportsGet = true)]
        public int AssemblyCount { get; set; }
        [BindProperty(SupportsGet = true)]
        public int ReorderCount { get; set; }
        public int PageSize { get; set; } = 10;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public int ReorderTotalPages => (int)Math.Ceiling(decimal.Divide(ReorderCount, PageSize));

        public int AssemblyTotalPages => (int)Math.Ceiling(decimal.Divide(AssemblyCount, PageSize));

        public IList<Item> Items { get; set; }

        public IList<Item> ReorderItems { get; set; }

        public IList<Item> AssembledItems { get; set; }


        public PagingInfo PagingInfo { get; set; }




        [BindProperty(SupportsGet = true)]
        public Item Item { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<int> Index { get; set; }

        [BindProperty(SupportsGet = true)]
        public int itemId { get; set; }//this is populated by the hidden input

        [BindProperty(SupportsGet = true)]
        public IList<AssemblyRecipe> AssemblyRecipes { get; set; }

        [BindProperty(SupportsGet = true)]
        public IList<RecipeLine> RecipeLines { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        //public TabContainer tb { get; set; }

        //public ViewResult List(int productPage = 1)
        //    => View(_context.Items
        //        .OrderBy(p => p.ProductID)
        //        .Skip((productPage - 1) * PageSize)
        //        .Take(PageSize));



        public async Task OnGetAsync(string sortOrder, int productPage = 1)
        {

            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            QtySort = sortOrder == "OnHandQty" ? "OnHandQty_desc" : "OnHandQty";
            VendorSort = sortOrder == "vendor" ? "vendor" : "vendor_desc";
            
            var items = from i in _context.Items
                        select i;

            var reorderItems = from i in _context.Items
                               where i.OnHandQty <= i.ReorderQty
                               select i;

            var assembledItems = from i in _context.Items
                                 where i.IsAssembled == true
                                 select i;



            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.ItemName);
                    break;
                case "OnHandQty":
                    items = items.OrderBy(s => s.OnHandQty);
                    break;
                case "OnHandQty_desc":
                    items = items.OrderByDescending(s => s.OnHandQty);
                    break;
                case "name":
                    items = items.OrderBy(s => s.ItemName);
                    break;
                case "vendor":
                    items = items.OrderBy(s => s.VendorId);
                    break;
                case "vendor_desc":
                    items = items.OrderByDescending(s => s.VendorId);
                    break;
                default:
                    //no reorder
                    break;
            }



            if (!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(s => s.ItemName.Contains(SearchString));
                reorderItems = reorderItems.Where(s => s.ItemName.Contains(SearchString));
                assembledItems = assembledItems.Where(s => s.ItemName.Contains(SearchString));
                PageSize = 50;
            }
            Count = items.Count();
            items = items.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
            Items = await items.AsNoTracking().ToListAsync();

           

            ReorderCount = reorderItems.Count();
            reorderItems = reorderItems.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
            ReorderItems = await reorderItems.AsNoTracking().ToListAsync();

           
            assembledItems = assembledItems.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
            AssembledItems = await assembledItems.AsNoTracking().ToListAsync();
            AssemblyCount = _context.AssemblyRecipes.Count();
            


            AssemblyRecipes = await _context.AssemblyRecipes.ToListAsync();
            RecipeLines = await _context.RecipeLines.ToListAsync();
            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == itemId);//get an item :D
            //ViewData["ItemName"] = new SelectList(_context.Items, "ItemId", "ItemName");

        }

        //////////////////////////////////////////////
        public async Task<IActionResult> OnPostAlphaAsync()
        {
            var items = from i in _context.Items
                        select i;
            if (!string.IsNullOrEmpty(SearchString))
            {
                items = items.Where(s => s.ItemName.Contains(SearchString));
            }

            Items = await items.ToListAsync();
            Items.OrderBy(item => item.OnHandQty);

            return RedirectToPage("/Inventory");
        }

        public async Task<IActionResult> OnPostAddAsync(int id)
        {

            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);
            Item.OnHandQty++;
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
            return RedirectToPage();
        }

        /// <summary>
        /// This is the method called by asp-page-handler="Subtract"
        /// it gets id from asp-route-id="some int"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPostSubtractAsync(int id, string tab = "")
        {

            Item = await _context.Items.FirstOrDefaultAsync(m => m.ItemId == id);

            //subtract one from it
            Item.OnHandQty--;
            //if(Item.OnHandQty < 0)
            //{
            //    Item.OnHandQty = 0;//no negative inventory
            //}
            //get db stuff, I copied the rest of this code from the scaffolding
            //so I can only follow it, not really create it yet
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
            //stay on current pageRedirectResult(Url.Action("Details", "OInfoes", new { id = phase.OID ,
            //tab = "phases"}));
            return RedirectToPage();
        }

        //used by createItemModal
        public async Task<IActionResult> OnPostCreateAsync()
        {

            Items = await _context.Items.ToListAsync();
            Item.LastModifiedDate = DateTime.Today;
            Item.LastModifiedBy = "AlphaTech Team";//this has to come out later to be replaced with whoever is logged in
            Item.MeasureID = 1;//this will need to be changed later

            if (!ModelState.IsValid)
            {
                return RedirectToPage();
            }

            //dont add duplicately named items
            foreach (var item in Items)
            {
                if (item.ItemName == Item.ItemName)
                {
                    //indicate failure due to identical items
                    return RedirectToPage();
                }
            }

            _context.Items.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }


        //used by getDetailsModal

        public bool getDetails(int? id)
        {
            if (id == null)
            {
                return false;
            }

            Item = _context.Items.FirstOrDefault(m => m.ItemId == id);

            if (Item == null)
            {
                return false;
            }
            Index.Add(Item.ItemId);
            return true;
        }

        public async Task<IActionResult> OnGetDetailsAsync(int? id)
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
            //Index = Items.IndexOf(Item);
            return RedirectToPage("Inventory", id);
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

    }
}