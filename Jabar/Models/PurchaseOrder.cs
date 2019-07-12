using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class PurchaseOrder
    {
        public int PurchaseOrderId { get; set; }
        public DateTime DateOrdered { get; set; }
        public string VendorPO { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //navigation(foriegn keys)
        public int VendorId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

    }
}
