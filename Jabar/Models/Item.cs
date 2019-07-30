using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class Item
    {

        public Item()
        {
            OrderItems = new HashSet<OrderItem>();
            InventoryLogs = new HashSet<InventoryLog>();
            AssemblyHistories = new HashSet<AssemblyHistory>();
            IsAssembled = false;
        }
        /// <summary>
        /// id for use in data context
        /// </summary>
        public int ItemId { get; set; }
        /// <summary>
        /// Name of item eg 'Drill' or 'Bolt'
        /// </summary>
        public string ItemName { get; set; }
        /// <summary>
        /// Description of the item
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// How many do we have on hand
        /// </summary>
        public int OnHandQty { get; set; }
        /// <summary>
        /// how much do they cost or are they sold for
        /// </summary>
        public double ListRetailCost { get; set; }
        /// <summary>
        /// Minimum amount to have onhand before 
        /// more need to be ordered..
        /// use to indicate to UI that reorder needs to happen
        /// </summary>
        public int ReorderQty { get; set; }
        /// <summary>
        /// The largest amount needed.
        /// if you have more than this qty you 
        /// DONT need to order more
        /// </summary>
        public int MaxQty { get; set; }
        /// <summary>
        ///  How much does it weigh
        ///  used for shipping costs
        /// </summary>
        public double MeasureAmnt { get; set; }
        /// <summary>
        /// who last modified this item
        /// </summary>
        public string LastModifiedBy { get; set; }
        /// <summary>
        /// when was this item last modified
        /// </summary>
        public DateTime LastModifiedDate { get; set; }

        //nav
        /// <summary>
        /// how is it measured... imperial or metric, eaches, per crate...
        /// </summary>
        public int MeasureID { get; set; }

        public bool IsAssembled { get; set; }

        public virtual Vendor PreferredVendor { get; set; }

        public int? AssemblyRecipeId { get; set; }

        public virtual AssemblyRecipe AssemblyRecipe { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }

        public IEnumerable<InventoryLog> InventoryLogs { get; set; }

        public IEnumerable<AssemblyHistory> AssemblyHistories { get; set; }
        //public IEnumerable<RecipeLine> RecipeLines { get; set; }

    }
}
