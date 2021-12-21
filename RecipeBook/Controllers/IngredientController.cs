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
    [Authorize]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Ingredients(uint? RecipeId)
        {
            try
            {
                var ingredient = await unitOfWork.Ingredients.GetAll(x => x.RecipeId == RecipeId.GetValueOrDefault());
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
        [HttpGet("{id:int}", Name = "Ingredient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Ingredient(int id)
        {
            try
            {
                var ingredient = await unitOfWork.Ingredients.Get(x => x.Id == id, new List<string> { "Recipe" });
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
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateIngredient([FromBody] CreateIngredientDto ingredientDto)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError($"Invalid Post attempts in {nameof(CreateIngredient)} of {nameof(IngredientController)}");
                return BadRequest(ModelState);
            }

            try
            {

                var ingredient = maper.Map<Ingredient>(ingredientDto);

                await unitOfWork.Ingredients.Insert(ingredient);
                await unitOfWork.Save();

                return CreatedAtRoute("Ingredient", new { Id = ingredient.Id }, ingredient);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(CreateIngredient)} of {nameof(IngredientController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }


        // PUT api/<IngredientController>/5
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateIngredient(uint Id, [FromBody] UpdateIngredientDto ingredientDto)
        {
            if (!ModelState.IsValid || Id < 1)
            {
                logger.LogError($"Invalid Update attempts in {nameof(UpdateIngredient)} of {nameof(IngredientController)}");
                return BadRequest(ModelState);
            }


            try
            {
                var ingredient = await unitOfWork.Ingredients.Get(q => q.Id == Id);

                if (ingredient == null)
                {
                    logger.LogError($"Invalid Update attempts in {nameof(UpdateIngredient)} of {nameof(IngredientController)}");
                    return BadRequest($"Submitted data is invalid.");
                }

                maper.Map(ingredientDto, ingredient);

                unitOfWork.Ingredients.Update(ingredient);

                await unitOfWork.Save();

                return Accepted(ingredient);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(UpdateIngredient)} of {nameof(IngredientController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }

        // DELETE api/<IngredientController>/5
        [HttpDelete("{id:int}")]

        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteIngredient(uint Id, [FromBody] UpdateIngredientDto ingredientDto)
        {
            if (!ModelState.IsValid || Id < 1)
            {
                logger.LogError($"Invalid Delete attempts in {nameof(DeleteIngredient)} of {nameof(IngredientController)}");
                return BadRequest(ModelState);
            }

            try
            {
                var ingredient = await unitOfWork.Ingredients.Get(q => q.Id == Id);

                if (ingredient == null)
                {
                    logger.LogError($"Invalid Delete attempts in {nameof(DeleteIngredient)} of {nameof(IngredientController)}");
                    return BadRequest($"Submitted data is invalid.");
                }

                await unitOfWork.Ingredients.Delete(Id);

                await unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error On {nameof(DeleteIngredient)} of {nameof(IngredientController)}");
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }
    }
}
