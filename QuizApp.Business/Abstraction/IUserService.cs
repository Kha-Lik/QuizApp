﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuizApp.Business.Models;

namespace QuizApp.Business.Abstraction
{
    public interface IUserService
    {
        Task<IdentityResult> Register(UserRegistrationModel model);

        Task<object> Login(UserLoginModel model);

        Task SignOut();

        IAsyncEnumerable<UserDto> GetUsersAsync();

        Task SetRoleAsync(UserDto model);

        IAsyncEnumerable<IdentityRole> GetRolesAsync();

    }
}