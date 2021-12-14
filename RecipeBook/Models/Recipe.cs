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
        [Key]
        public uint Id { get; set; }
        [Required()]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [NotMapped]
        public List<Ingredient> Employees { get; set; }
    }
}
