using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Jabar.Models
{
    public class Assembly
    {

        public Assembly()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssemblyId { get; set; }
        public int AssemblyRecipeId { get; set; }


        public string AssemblyName { get; set; }        
        public string Description { get; set; }        
        public int OnHandQty { get; set; }
        

        public virtual AssemblyRecipe AssemblyRecipe { get; set; }

    }
}
