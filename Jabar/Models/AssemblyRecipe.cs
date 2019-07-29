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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssemblyRecipeId { get; set; }
        public int AssemblyId { get; set; }

        public virtual Assembly Assembly { get; set; }
       
        public virtual IEnumerable<RecipeLine> RecipeLines { get; set; }
    }
}
