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
        public int ItemId { get; set; }
        public int RequiredItemQty { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }


        //nav
       
        //public int AssemblyRecipeId { get; set; }
        
/* 
        [ForeignKey("AssemblyRecipeId")]
        public virtual AssemblyRecipe AssemblyRecipe { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

        SqlException: The INSERT statement conflicted with the FOREIGN KEY constraint 
         * "FK_RecipeLines_AssemblyRecipes_AssemblyRecipeId". The conflict occurred in 
         * database "WhoLives_Jabar", table "dbo.AssemblyRecipes", column 'AssemblyRecipeId'.
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
