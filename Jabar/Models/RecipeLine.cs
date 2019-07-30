using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class RecipeLine
    {
        public RecipeLine()
        {

        }
       
        public int RecipeLineId { get; set; }       
        public int ItemId { get; set; }  
        public int AssemblyRecipeId { get; set; }
        

        public int RequiredItemQty { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public virtual Item Item { get; set; }
        public virtual AssemblyRecipe AssemblyRecipe { get; set; }

    }
}
