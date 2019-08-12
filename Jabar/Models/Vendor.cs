﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class Vendor
    {
        //
        public Vendor()
        {

        }

        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public string VendorAddress { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //nav
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; }

    }
}
