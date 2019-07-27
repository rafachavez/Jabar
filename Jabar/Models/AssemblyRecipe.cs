using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class AssemblyRecipe
    {
        public int AssemblyRecipeId { get; set; }

        public int ItemId { get; set; }
       
        public IEnumerable<RecipeLine> RecipeLines { get; set; }
    }
}
