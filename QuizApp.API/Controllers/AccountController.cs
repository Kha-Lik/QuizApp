using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("Sign in")]
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

        [HttpPost("Sign up")]
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

        [HttpPost("Sign Out")]
        public async Task<ActionResult> SignOut()
        {
            await _userService.SignOut();
            return Ok();
        }
    }
}
