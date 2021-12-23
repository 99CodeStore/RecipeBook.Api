using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Configuration.Entities;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Data
{
    public class RecipeBookDbContext : IdentityDbContext<ApiUser>
    {
        public RecipeBookDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());

            builder.ApplyConfiguration(new RecipeConfiguration());
            builder.ApplyConfiguration(new IngredientConfiguration());

        }

    }
}
