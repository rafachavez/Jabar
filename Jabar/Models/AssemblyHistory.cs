using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class AssemblyHistory
    {
        public int AssemblyHistoryId { get; set; }
        public DateTime AssemblyDate { get; set; }
        public int QtyAssembled { get; set; }
        public string Notes { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        //nav
        public int ItemId { get; set; }
    }
}
