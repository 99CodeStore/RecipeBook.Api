using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    [Table("Recipes")]
    public class Recipe
    {
        public Recipe(string Name,string Description,string ImageUrl, List<Ingredient> Ingredients)
        {
            this.Name = Name;
            this.Description = Description;
            this.ImageUrl = ImageUrl;
            this.Ingredients = Ingredients ?? new List<Ingredient>();
        }
        public Recipe()
        {
            this.Ingredients = new List<Ingredient>();
        }
        [Key]
        public uint Id { get; set; }
        [Required()]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public List<Ingredient> Ingredients { get; set; }
    }
}
