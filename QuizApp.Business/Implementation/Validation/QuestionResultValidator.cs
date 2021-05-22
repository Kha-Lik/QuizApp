using FluentValidation;
using QuizApp.Business.Models;

namespace QuizApp.Business.Implementation.Validation
{
    public class QuestionResultValidator : AbstractValidator<QuestionResultDto>
    {
        public QuestionResultValidator()
        {
            RuleFor(qr => qr.Id).NotNull();
            RuleFor(qr => qr.QuestionId).NotNull();
            RuleFor(qr => qr.AttemptId).NotNull();
        }
    }
}