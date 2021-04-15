using Microsoft.Extensions.DependencyInjection;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Models;
using QuizApp.Business.Implementation.Services;
using QuizApp.Business.Implementation.Validation;
using FluentValidation;

namespace QuizApp.Business.Implementation
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBllServices(this IServiceCollection services)
        {

            services.AddTransient<ICrudInterface<SubjectDto>, SubjectService>()
                .AddTransient<ICrudInterface<TopicDto>, TopicService>()
                .AddTransient<ICrudInterface<QuestionDto>, QuestionService>()
                .AddTransient<ICrudInterface<AnswerDto>, AnswerService>()
                .AddTransient<ICrudInterface<AttemptDto>, AttemptService>()
                .AddTransient<ICrudInterface<QuestionResultDto>, QuestionResultService>();

            services.AddTransient(typeof(IServiceHelper<>), typeof(ServiceHelper<>));

            services.AddTransient<AbstractValidator<SubjectDto>, SubjectValidator>()
                .AddTransient<AbstractValidator<TopicDto>, TopicValidator>()
                .AddTransient<AbstractValidator<QuestionDto>, QuestionValidator>()
                .AddTransient<AbstractValidator<AnswerDto>, AnswerValidator>()
                .AddTransient<AbstractValidator<AttemptDto>, AttemptValidator>()
                .AddTransient<AbstractValidator<QuestionResultDto>, QuestionResultValidator>();

            return services;
        }
    }
}