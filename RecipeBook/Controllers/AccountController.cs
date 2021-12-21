using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeBook.Data;
using RecipeBook.Models;
using RecipeBook.Services;
using System;
using System.Threading.Tasks;

namespace RecipeBook.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly ILogger<AccountController> logger;
        private readonly IMapper mapper;
        private readonly IAuthManager authManager;

        public AccountController(UserManager<ApiUser> userManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthManager authManager
            )
        {
            this.userManager = userManager;
            this.logger = logger;
            this.mapper = mapper;
            this.authManager = authManager;
        }
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody] UserDto userDto)
        {
            logger.LogInformation($"Registration attempts for {userDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = mapper.Map<ApiUser>(userDto);
                user.UserName = userDto.Email;
                var result = await userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
                await userManager.AddToRolesAsync(user, userDto.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Registration attempts for {userDto.Email}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            logger.LogInformation($"Login attempts for {loginDto.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                if (!await authManager.ValidateUser(loginDto))
                {
                    return Unauthorized();
                }

                return Accepted(new { Token = await authManager.CreateToken() });
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Login attempts for {loginDto.Email}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

    }
}
