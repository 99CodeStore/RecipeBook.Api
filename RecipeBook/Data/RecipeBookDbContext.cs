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
        {

        }

        DbSet<Recipe> Recipes { get; set; }
        DbSet<Ingredient> Ingredients { get; set; }
    }
}
