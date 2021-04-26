using Microsoft.AspNetCore.Identity;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizApp.DataAccess.Implementation;
using QuizApp.DataAccess.Entities;
using QuizApp.Business.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Business.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuizApp.Business.Implementation.Exceptions;
using FluentValidation;

namespace QuizApp.Business.Implementation.Services
{
    public class UserService : IUserService
    {
        private readonly QuizDbContext _quizDbContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly AbstractValidator<UserDto> _validator;

        public UserService(QuizDbContext dbContext, SignInManager<User> signInManager, UserManager<User> userManager, IOptions<JwtSettings> jwtSettings,
            AbstractValidator<UserDto> validator, RoleManager<IdentityRole> roleManager)
        {
            _quizDbContext = dbContext;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _validator = validator;
        }
        public async Task<object> Login(UserLoginModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
                return GenerateJwtToken(model.Email, user);
            }
            throw new LoginException("Login failed");
        }

        public async Task<IdentityResult> Register(UserRegistrationModel model)
        {
            var user = UserMapper.AdaptToUser(model);
            var result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public IAsyncEnumerable<UserDto> GetUsersAsync()
        {
            return _quizDbContext.Users.Select(UserMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public async Task SetRoleAsync(UserDto model)
        {
            var user = await _quizDbContext.Users.FindAsync(model.Id);
            if (user != null)
            {
                await _validator.ValidateAsync(model);

                _quizDbContext.Users.Update(model.AdaptToUser());
                await _userManager.AddToRoleAsync(user, model.Role);
                await _quizDbContext.SaveChangesAsync();
            }
        }

        public IAsyncEnumerable<IdentityRole> GetRolesAsync()
        {
            return _roleManager.Roles.ToAsyncEnumerable();
        }

        private object GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.Add(_jwtSettings.Lifetime);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
