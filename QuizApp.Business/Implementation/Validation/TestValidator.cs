using FluentValidation;
using QuizApp.Business.Models;

namespace QuizApp.Business.Implementation.Validation
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(t => t.Student).NotNull();
            RuleFor(t => t.Questions).NotNull().NotEmpty();
            RuleFor(t => t.Topic).NotNull();
        }
    }
}