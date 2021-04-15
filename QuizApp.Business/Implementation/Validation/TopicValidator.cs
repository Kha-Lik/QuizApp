using FluentValidation;
using QuizApp.Business.Models;

namespace QuizApp.Business.Implementation.Validation
{
    public class TopicValidator : AbstractValidator<TopicDto>
    {
        public TopicValidator()
        {
            RuleFor(t => t.Id).NotNull();
            RuleFor(t => t.Name).NotNull();
            RuleFor(t => t.MaxAttemptCount).GreaterThan(0);
            RuleFor(t => t.QuestionsPerAttempt).GreaterThanOrEqualTo(0);
            RuleFor(t => t.TimeToPass).GreaterThan(0);
            RuleFor(t => t.TopicNumber).GreaterThan(0);
            RuleFor(t => t.SubjectId).NotNull();
        }
    }
}