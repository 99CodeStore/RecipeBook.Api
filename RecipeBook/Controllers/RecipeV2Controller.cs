using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Data;
using RecipeBook.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipeBook.Controllers
{
     
    [ApiVersion("2.0")]
    [Route("[controller]")]
    [ApiController]
    public class RecipeV2Controller : ControllerBase
    {
        private readonly RecipeBookDbContext dbContext;

        public RecipeV2Controller(RecipeBookDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRecipes([FromQuery] PagedRequestInput pagedRequest)
        {

            var recipes = dbContext.Recipes;

            return Ok(recipes);
        }
    }
}
