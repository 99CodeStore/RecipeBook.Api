using RecipeBook.Models;
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
