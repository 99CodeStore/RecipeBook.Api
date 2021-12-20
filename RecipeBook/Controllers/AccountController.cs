using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecipeBook.Data;
using RecipeBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeBook.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> userManager;
        private readonly SignInManager<ApiUser> signInManager;
        private readonly ILogger<AccountController> logger;
        private readonly IMapper mapper;

        public AccountController(UserManager<ApiUser> userManager,
            SignInManager<ApiUser> signInManager,
            ILogger<AccountController> logger,
            IMapper mapper
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.mapper = mapper;
        }
        [HttpPost]
        [Route("Register")]
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
                var result = await userManager.CreateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest($"User Registration attempts failed.");
                }
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

                var result = await signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Passsword,
                    false, false);

                if (!result.Succeeded)
                {
                    return Unauthorized(loginDto);
                }
                return Accepted();
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Login attempts for {loginDto.Email}");
                return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
            }
        }

    }
}
