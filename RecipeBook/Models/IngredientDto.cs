using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class IngredientDto : CreateIngredientDto
    {
        [Required()]
        public uint Id { get; set; }
         
    }

    public class UpdateIngredientDto : CreateIngredientDto
    {

    }
}
