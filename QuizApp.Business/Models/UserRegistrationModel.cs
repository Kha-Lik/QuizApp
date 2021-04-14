using System.ComponentModel.DataAnnotations;

namespace QuizApp.Business.Models
{
    public class UserRegistrationModel
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords should be equal.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}