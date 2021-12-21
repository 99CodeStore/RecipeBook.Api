using RecipeBook.Models;
using System.Threading.Tasks;

namespace RecipeBook.Services
{
    public interface IAuthManager
    {

        Task<bool> ValidateUser(LoginDto loginDto);
        Task<string> CreateToken();

    }
}