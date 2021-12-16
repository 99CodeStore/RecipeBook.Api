using RecipeBook.Data;
using RecipeBook.IRepository;
using RecipeBook.Models;
using System;
using System.Threading.Tasks;

namespace RecipeBook.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RecipeBookDbContext recipeBookDbContext;
        private IGenericRepository<Recipe> _recipes;
        private IGenericRepository<Ingredient> _ingredients;


        public UnitOfWork(RecipeBookDbContext recipeBookDbContext)
        {
            this.recipeBookDbContext = recipeBookDbContext;
        }

        public IGenericRepository<Recipe> Recipes => _recipes ??= new GenericRepository<Recipe>(recipeBookDbContext);

        public IGenericRepository<Ingredient> Ingredients => _ingredients ??= new GenericRepository<Ingredient>(recipeBookDbContext);

        public void Dispose()
        {
            recipeBookDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await recipeBookDbContext.SaveChangesAsync();
        }
    }
}
