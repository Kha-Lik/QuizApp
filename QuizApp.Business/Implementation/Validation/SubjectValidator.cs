using FluentValidation;
using QuizApp.Business.Models;

namespace QuizApp.Business.Implementation.Validation
{
    public class SubjectValidator : AbstractValidator<SubjectDto>
    {
        public SubjectValidator()
        {
            RuleFor(s => s.Id).NotNull();
            RuleFor(s => s.Name).NotNull();
            RuleFor(s => s.LecturerId).NotNull();
        }
    }
}