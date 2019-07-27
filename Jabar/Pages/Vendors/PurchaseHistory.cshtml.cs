using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jabar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Jabar.Pages.Vendors
{
    public class PurchaseHistoryModel : PageModel
    {
        private readonly Jabar.Data.ApplicationDbContext _context;

        public PurchaseHistoryModel(Jabar.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Vendor Vendor { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<PurchaseOrderList> PurchaseItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            // Validate Id was passed in
            if(id == null)
            {
                return NotFound();
            }

            // Populate and validate Vendor Info
            Vendor = await _context.Vendors.FirstOrDefaultAsync(m => m.VendorId == id);

            if (Vendor == null)
            {
                return NotFound();
            }

            // Populate and validate Purchase Orders
            PurchaseOrders = await _context.PurchaseOrders.Where(m => m.VendorId == id).OrderBy(m => m.DateOrdered).ToListAsync();

            if(PurchaseOrders == null)
            {
                return NotFound();
            }

            // Populate and validate Purchase Order Items
            // If the size and number of orders get too large this may need to be replaced with on the fly invoice retrieval when an order is selected
            // For the time being, we are grabbing all invoices associated with a given vendor
            PurchaseItems = new List<PurchaseOrderList>();
            foreach(var p in PurchaseOrders)
            {
                PurchaseItems.Add(new PurchaseOrderList
                {
                    PurchaseOrderId = p.PurchaseOrderId,
                    PurchaseOrderItems = await _context.OrderItems.Where(m => m.PurchaseOrderId == p.PurchaseOrderId).OrderBy(m => m.ItemId).ToListAsync()
                });
            }

            foreach(var l in PurchaseItems)
            {
                if(l.PurchaseOrderItems == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }
    }

    public class PurchaseOrderList
    {
        public int PurchaseOrderId { get; set; }
        public List<OrderItem> PurchaseOrderItems { get; set; }
    }
}