using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace QuizApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("SignIn")]
        public async Task<ActionResult> Login(UserLoginModel model)
        {
            try
            {
                var token = await _userService.Login(model);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult> Register(UserRegistrationModel model)
        {
            try
            {
                var result = await _userService.Register(model);

                if (!result.Succeeded)
                {
                    string errors = result.Errors.Aggregate("", (current, identityError) => current + (identityError.Description + "\n"));
                    return BadRequest(errors);
                }

                UserLoginModel loginModel = new UserLoginModel
                {
                    Email = model.Email,
                    Password = model.Password,
                    RememberMe = false
                };

                var token = await _userService.Login(loginModel);
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Logout")]
        public async Task<ActionResult> Logout()
        {
            await _userService.SignOut();
            return Ok();
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("Users")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return await _userService.GetUsersAsync().ToListAsync(); 
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet("Roles")]
        public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRoles()
        {
            return await _userService.GetRolesAsync().ToListAsync();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut]
        public async Task<ActionResult> SetRoleAsync(UserDto model)
        {
            try
            {
                await _userService.SetRoleAsync(model);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(model);
        }
    }
}
