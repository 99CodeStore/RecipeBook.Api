using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeBook.Data;
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
        [HttpGet("{id:int}",Name = "GetRecipe")]
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
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateRecipe([FromBody] CreateRecipeDto recipeDto)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError($"Invalid Post attempts in {nameof(CreateRecipe)} of {nameof(RecipeController)}");
                return BadRequest(ModelState);
            }

            try
            {

                var recipe = maper.Map<Recipe>(recipeDto);

                await unitOfWork.Recipes.Insert(recipe);
                await unitOfWork.Save();

                return CreatedAtRoute("GetRecipe",new { Id=recipe.Id}, recipe);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(CreateRecipe)} of {nameof(RecipeController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // PUT api/<RecipeController>/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateRecipe(uint Id, [FromBody] UpdateRecipeDto recipeDto)
        {
            if (!ModelState.IsValid || Id < 1)
            {
                logger.LogError($"Invalid Update attempts in {nameof(UpdateRecipe)} of {nameof(RecipeController)}");
                return BadRequest(ModelState);
            }

            try
            {
                var recipe = await unitOfWork.Recipes.Get(q => q.Id == Id);

                if (recipe == null)
                {
                    logger.LogError($"Invalid Update attempts in {nameof(UpdateRecipe)} of {nameof(RecipeController)}");
                    return BadRequest($"Submitted data is invalid.");
                }

                maper.Map(recipeDto, recipe);

                unitOfWork.Recipes .Update(recipe);

                await unitOfWork.Save();

                return Accepted(recipe);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(UpdateRecipe)} of {nameof(IngredientController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE api/<RecipeController>/5
        [HttpDelete("{id:int}")]

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteRecipe(uint Id, [FromBody] UpdateRecipeDto recipeDto)
        {
            if (!ModelState.IsValid || Id < 1)
            {
                logger.LogError($"Invalid Delete attempts in {nameof(DeleteRecipe)} of {nameof(RecipeController)}");
                return BadRequest(ModelState);
            }

            try
            {
                var recipe = await unitOfWork.Recipes.Get(q => q.Id == Id);

                if (recipe == null)
                {
                    logger.LogError($"Invalid Delete attempts in {nameof(DeleteRecipe)} of {nameof(RecipeController)}");
                    return BadRequest($"Submitted data is invalid.");
                }

                await unitOfWork.Recipes.Delete(Id);

                await unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(DeleteRecipe)} of {nameof(RecipeController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

    }
}
