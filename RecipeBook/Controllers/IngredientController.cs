using AutoMapper;
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
    [Route("[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<RecipeController> logger;
        private readonly IMapper maper;

        public IngredientController(IUnitOfWork unitOfWork,
            ILogger<RecipeController> logger,
            IMapper maper)
        {
            this.unitOfWork = unitOfWork;
            this.logger = logger;
            this.maper = maper;
        }
        // GET: api/<IngredientController>
        [HttpGet("recipeIdBy/{recipeId}")]
        [ProducesResponseType( StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Ingredients(uint? RecipeId)
        {
            try
            {
                var ingredient = await unitOfWork.Ingredients.GetAll(x=>x.RecipeId==RecipeId.GetValueOrDefault());
                var result = maper.Map<IList<IngredientDto>>(ingredient);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(Ingredients)} of {nameof(IngredientController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // GET api/<IngredientController>/5
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Ingredient(int id)
        {
            try
            {
                var ingredient = await unitOfWork.Ingredients.Get(x => x.Id == id,new List<string> { "Recipe" });
                var result = maper.Map<IngredientDto>(ingredient);
                return Ok(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(Ingredients)} of {nameof(IngredientController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // POST api/<IngredientController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<IngredientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
