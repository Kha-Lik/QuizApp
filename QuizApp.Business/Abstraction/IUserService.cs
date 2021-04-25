using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuizApp.Business.Models;

namespace QuizApp.Business.Abstraction
{
    public interface IUserService
    {
        Task<object> Register(UserRegistrationModel model);

        Task<object> Login(UserLoginModel model);

        Task SignOut();
    }
}