using RecipeBook.Data;
using System;
using System.Threading.Tasks;

namespace RecipeBook.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save();
        IGenericRepository<Recipe> Recipes { get; }
        IGenericRepository<Ingredient> Ingredients { get; }
    }
    
}
