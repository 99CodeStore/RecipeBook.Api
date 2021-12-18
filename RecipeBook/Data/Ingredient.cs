using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Data
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        public string Name { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey(nameof(Recipe))]
        public uint RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public Ingredient(uint id,string Name,decimal Amount,uint RecipeId=0)
        {
            this.Id = id;
            this.Name = Name;
            this.Amount = Amount;
            this.RecipeId = RecipeId;
        }
    }
}
