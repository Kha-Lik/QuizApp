using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QuizApp.Business.Models;

namespace QuizApp.Business.Abstraction
{
    public interface IUserService
    {
        Task<IdentityResult> Register(UserRegistrationModel model);

        Task<SignInResult> Login(UserLoginModel model);

        Task SignOut();
    }
}