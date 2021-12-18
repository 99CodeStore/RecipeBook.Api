using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public class RecipeBookDbContext : DbContext
    {
        public RecipeBookDbContext(DbContextOptions options) : base(options)
        { }

        DbSet<Recipe> Recipes { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Recipe>().HasData(
                    new Recipe()
                    {
                        Id = 1,
                        Name = "जलेबी",
                        Description = "जलेबी उत्तर भारत, पाकिस्तान व मध्यपूर्व का एक लोकप्रिय व्यंजन है। इसका आकार पेंचदार होता है और स्वाद करारा मीठा। इस मिठाई की धूम भारतीय उपमहाद्वीप से शुरू होकर पश्चिमी देश स्पेन तक जाती है। इस बीच भारत, बांग्लादेश, पाकिस्तान, ईरान के साथ तमाम अरब मुल्कों में भी यह खूब जानी-पहचानी है।",
                        ImageUrl = "https://punampaul.com/wp-content/uploads/2019/10/Jalebi-Recipe.jpg",
                        Ingredients = new List<Ingredient>()
                    },

                    new Recipe()
                    {
                        Id = 2,
                        Name = "समोसा",
                        Description = "समोसा एक तला हुआ या बेक किया हुआ भरवां अल्पाहार व्यंजन है। इसमें प्रायः मसालेदार भुने या पके हुए सूखे आलू, या इसके अलावा मटर, प्याज, दाल, कहीम कहीं मांसा भी भरा हो सकता है। इसका आकार प्रायः तिकोना होता है किन्तु आकार और नाप भिन्न-भिन्न स्थानों पर बदल सकता है। अधिकतर ये चटनी के संग परोसे जाते हैं।",
                        ImageUrl = "https://static.langimg.com/thumb/msid-76956747,width-680,resizemode-3/navbharat-times.jpg",
                        Ingredients = new List<Ingredient>()
                    }

                );

            builder.Entity<Ingredient>().HasData(
                            new Ingredient(1,"चीनी", 5, 1),
                            new Ingredient(2,"मैदा", 5, 1),
                            new Ingredient(3,"तेल", 5, 1),

                            new Ingredient(4,"पके सूखे आलू", 5, 2),
                            new Ingredient(5,"मटर", 2, 2),
                            new Ingredient(6,"प्याज", 2, 2),
                            new Ingredient(7,"दाल", 2, 2),
                            new Ingredient(8,"चटनी", 3, 2)
                );
        }

    }
}
