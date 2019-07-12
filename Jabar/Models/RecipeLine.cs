using System;
using System.Collections.Generic;
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


    }
}
