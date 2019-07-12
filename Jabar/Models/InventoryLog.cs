using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class InventoryLog
    {
        public int InventoryLogId { get; set; }
        public int PreviousQty { get; set; }
        public int NewQty { get; set; }
        public bool Reconciled { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //nav
        public int ItemId { get; set; }
    }
}
