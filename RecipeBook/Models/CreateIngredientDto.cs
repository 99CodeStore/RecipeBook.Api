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

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public int RecipeId { get; set; }
    }
}
