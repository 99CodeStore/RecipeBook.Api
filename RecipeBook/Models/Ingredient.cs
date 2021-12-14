using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public Ingredient(string Name,decimal Amount)
        {
            this.Name = Name;
            this.Amount = Amount;
        }
    }
}
