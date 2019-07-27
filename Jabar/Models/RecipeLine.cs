using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class RecipeLine
    {
        public int RecipeLineId { get; set; }
        public int RequiredItemQty { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }


        //nav
       
        public int AssemblyRecipeId { get; set; }
        public int ItemId { get; set; }

        [ForeignKey("AssemblyRecipeId")]
        public virtual AssemblyRecipe Recipe { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        /*
         *  [Display(Name="Product Type")]
        public int ProductTypeId { get; set; }

        [Display(Name="Special Tag")]
        public int SpecialTagsId { get; set; }

        //virtual assumes the table already exists... lazy loading
        [ForeignKey("SpecialTagsId")]
        public virtual SpecialTags SpecialTags { get; set; }


        [ForeignKey("ProductTypeId")]
        public virtual ProductTypes ProductTypes { get; set; }
         */

    }
}
