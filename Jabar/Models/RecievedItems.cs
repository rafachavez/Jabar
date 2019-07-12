using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class RecievedItems
    {
        public int RecievedItemsId { get; set; }
        public int QuantityRecieved { get; set; }
        public string Notes { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //nav
        public int OrderItemId { get; set; }
    }
}
