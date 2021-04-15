using FluentValidation;
using QuizApp.Business.Models;

namespace QuizApp.Business.Implementation.Validation
{
    public class AttemptValidator : AbstractValidator<AttemptDto>
    {
        public AttemptValidator()
        {
            RuleFor(at => at.Id).NotNull();
            RuleFor(at => at.Score).GreaterThanOrEqualTo(0);
            RuleFor(at => at.DateTime).NotNull();
            RuleFor(at => at.StudentId).NotNull();
            RuleFor(at => at.TopicId).NotNull();
        }
    }
}