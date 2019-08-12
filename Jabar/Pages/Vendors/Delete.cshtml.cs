using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Jabar.Data;
using Jabar.Models;

namespace Jabar.Pages.Vendors
{
    public class DeleteModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public DeleteModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Vendor Vendor { get; set; }

        [BindProperty(SupportsGet =true)]
        public IList<Item> Items { get; set; }

        [BindProperty(SupportsGet =true)]
        public IList<Vendor> Vendors { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Vendor = await _context.Vendors.FirstOrDefaultAsync(m => m.VendorId == id);

            if (Vendor == null)
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

            Vendor = await _context.Vendors.FindAsync(id);
            Items = await _context.Items.ToListAsync();
            Vendors = await _context.Vendors.ToListAsync();
            //if user is trying to delete the last vendor just make it again
            if (Vendors.Count <= 1)
            {
                Vendor defaultVendor = new Vendor();
                defaultVendor.VendorName = "No Preferred Vendor";
                defaultVendor.LastModifiedBy = "Please don't delete this";
                defaultVendor.LastModifiedDate = DateTime.Today;
                Vendors.Insert(0, defaultVendor);
                _context.Attach(Vendors).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
            }

            foreach (var item in Items)
            {
                if(item.VendorId == Vendor.VendorId)
                {
                    Vendor vendor =  _context.Vendors.FirstOrDefault(v=>v.VendorName == "Vendor Deleted");

                    if(vendor == null)
                    {
                        vendor = new Vendor();
                        vendor.VendorName = "Vendor Deleted";
                        vendor.LastModifiedBy = "AphlaTech db protection";//maybe change to user name
                        vendor.LastModifiedDate = DateTime.Today;
                    }

                    item.PreferredVendor = vendor;
                    item.VendorId = vendor.VendorId;
                    item.LastModifiedBy = "db change"; //change to user
                    item.LastModifiedDate = DateTime.Today;
                    _context.Attach(item).State = EntityState.Modified;
                }
            }
            
            

            if (Vendor != null)
            {
                _context.Vendors.Remove(Vendor);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
