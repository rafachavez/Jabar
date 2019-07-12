using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public string VendorSKU { get; set; }
        //maybe change to double?
        public double Price { get; set; }
        public int QuantityOrdered { get; set; }
        public DateTime? DateDelivered { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //nav
        public int ItemId { get; set; }
        public int PurchaseOrderId { get; set; }

        public IEnumerable<RecievedItems> RecievedItems { get; set; }
    }
}
