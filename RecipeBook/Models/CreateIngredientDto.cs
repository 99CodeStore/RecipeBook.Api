using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class CreateIngredientDto
    {

        [Required]
        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
