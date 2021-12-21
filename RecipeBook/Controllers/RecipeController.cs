using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeBook.IRepository;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeBook.Controllers
{
    //[Route("api/[controller]")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<RecipeController> logger;
        private readonly IMapper maper;

        public RecipeController(IUnitOfWork unitOfWork,
            ILogger<RecipeController> logger,
            IMapper maper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.maper = maper;
        }
        // GET: api/<RecipeController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRecipes()
        {
            try
            {
                var recipes = await unitOfWork.Recipes.GetAll();
                var result = maper.Map<IList<RecipeDto>>(recipes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(GetRecipes)} of {nameof(RecipeController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET api/<RecipeController>/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetRecipe(int id)
        {
            try
            {
                var recipe = await unitOfWork.Recipes.Get(x => x.Id == id,
                    new List<string> { "Ingredients" }
                    );
                var result = maper.Map<RecipeDto>(recipe);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(GetRecipe)} of {nameof(RecipeController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST api/<RecipeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
