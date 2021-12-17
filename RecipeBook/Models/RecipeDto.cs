using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class CreateRecipeDto
    {
        [Required]
        [StringLength(maximumLength: 250, ErrorMessage = "Recipe Name is too long.")]

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public List<CreateIngredientDto> Ingredients { get; set; }
    }

    public class RecipeDto : CreateRecipeDto
    {
        [Required()]
        public uint Id { get; set; }

    }
}
