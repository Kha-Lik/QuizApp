using FluentValidation;
using QuizApp.Business.Models;

namespace QuizApp.Business.Implementation.Validation
{
    public class QuestionValidator : AbstractValidator<QuestionDto>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.Id).NotNull();
            RuleFor(q => q.QuestionNumber).GreaterThan(0);
            RuleFor(q => q.TopicId).NotNull();
        }
    }
}