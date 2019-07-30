using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class AssemblyRecipe
    {
        public AssemblyRecipe()
        {
            RecipeLines = new HashSet<RecipeLine>();
        }

        
        public int AssemblyRecipeId { get; set; }
        public int ItemId { get; set; }//change this to ItemId for items

        public virtual Item Item { get; set; }//change this to public virtual Item Item
       
        public virtual IEnumerable<RecipeLine> RecipeLines { get; set; }
    }
}
