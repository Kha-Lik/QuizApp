using FluentValidation;
using QuizApp.Business.Models;

namespace QuizApp.Business.Implementation.Validation
{
    public class AnswerValidator : AbstractValidator<AnswerDto>
    {
        public AnswerValidator()
        {
            RuleFor(a => a.Id).NotNull();
            RuleFor(a => a.AnswerText).NotEmpty();
            RuleFor(a => a.QuestionId).NotEmpty();
        }
    }
}