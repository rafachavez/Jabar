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

namespace Jabar.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public CreateModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        //This is for the Vendor select list
        [BindProperty(SupportsGet = true)]
        public Vendor VendorsModel { get; set; }
        public List<SelectListItem> VendorsModelId { get; set; }
        public IList<Vendor> VendorList { get; set; }

        //These are for the item list
        [BindProperty(SupportsGet = true)]
        public Item Items { get; set; }
        public List<SelectListItem> ItemName { get; set; }
        public IList<Item> ItemList { get; set; }
        [BindProperty(SupportsGet = true)]
        public IList<int> Index { get; set; }
        public IActionResult OnGet()
        {
            VendorsModelId = _context.Vendors.Select(x => new SelectListItem
            {
                Value = x.VendorId.ToString(),
                Text = x.VendorName
            })
                .ToList();

            ItemName = _context.Items.Select(x => new SelectListItem
            {
                Value = x.ItemId.ToString(),
                Text = x.ItemName
            })
                .ToList();
            return Page();
        }

        [BindProperty]
        public OrderItem OrderItem { get; set; }
        [BindProperty]
        public PurchaseOrder PurchaseOrder { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderItems.Add(OrderItem);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        /// <summary>
        /// Item Create
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostCreateAsync()
        {
            //NewVendor
            if(VendorsModel.VendorName != null)
            {
                VendorList = await _context.Vendors.ToListAsync();
                VendorsModel.LastModifiedDate = DateTime.Today;
                VendorsModel.LastModifiedBy = "AlphaTech Team";

                if (!ModelState.IsValid)
                {
                    return RedirectToPage();
                }

                foreach(var item in VendorList)
                {
                    if(item.VendorName == VendorsModel.VendorName)
                    {
                        return RedirectToPage();
                    }
                }
                _context.Vendors.Add(VendorsModel);
                await _context.SaveChangesAsync();
            }
            

            //New Item 
            if (Items.ItemName != null)
            {
                ItemList = await _context.Items.ToListAsync();
            
                Items.LastModifiedDate = DateTime.Today;
                Items.LastModifiedBy = "AlphaTech Team";//this has to come out later to be replaced with whoever is logged in
                Items.MeasureID = 1;//this will need to be changed later

                if (!ModelState.IsValid)
                {
                    return RedirectToPage();
                }

                //dont add duplicately named items
                foreach (var item in ItemList)
                {
                    if (item.ItemName == Items.ItemName)
                    {
                        //indicate failure due to identical items
                        return RedirectToPage();
                    }
                }

                _context.Items.Add(Items);
                await _context.SaveChangesAsync();

            }
            

            return RedirectToPage();
        }

    }
}