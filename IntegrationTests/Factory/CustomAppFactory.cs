using System;
using System.Collections.Generic;
using System.Linq;
using QuizApp.DataAccess.Entities;
using QuizApp.DataAccess.Implementation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.API;

namespace QuizApp.IntegrationTests.Factory
{
    internal class CustomAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                RemoveShopDbContextRegistration(services);

                var serviceProvider = GetInMemoryServiceProvider();

                services.AddDbContextPool<QuizDbContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.Empty.ToString());
                    options.UseInternalServiceProvider(serviceProvider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<QuizDbContext>();

                SeedData(context);
            });
        }

        private void SeedData(QuizDbContext context)
        {
            context.Subjects.Add(new Subject { Id = "subject1", Name = "Subject1", LecturerId = "1" });
            context.Subjects.Add(new Subject { Id = "subject2", Name = "Subject2", LecturerId = "2" });
            context.Topics.Add(new Topic { Id = "topic1", Name = "Topic1", MaxAttemptCount = 3, QuestionsPerAttempt = 1, TimeToPass = 5, TopicNumber = 1, SubjectId = "subject1" });
            context.Topics.Add(new Topic { Id = "topic2", Name = "Topic2", MaxAttemptCount = 2, QuestionsPerAttempt = 2, TimeToPass = 3, TopicNumber = 2, SubjectId = "subject2" });
            context.Questions.Add(new Question { Id = "question1", QuestionNumber = 1, TopicId = "topic1", QuestionText = "Where?" });
            context.Questions.Add(new Question { Id = "question2", QuestionNumber = 3, TopicId = "topic2", QuestionText = "Why?" });
            context.Answers.Add(new Answer { Id = "answer1", AnswerText = "Here", IsCorrect = true, QuestionId = "question1" });
            context.Answers.Add(new Answer { Id = "answer2", AnswerText = "There", IsCorrect = false, QuestionId = "question1" });
            context.Answers.Add(new Answer { Id = "answer3", AnswerText = "Because", IsCorrect = true, QuestionId = "question2" });
            context.Answers.Add(new Answer { Id = "answer4", AnswerText = "London is the capital of Great Britain", IsCorrect = false, QuestionId = "question2" });
            context.Attempts.Add(new Attempt { Id = "attempt1", DateTime = DateTime.Now, Score = 100, StudentId = "1", TopicId = "topic2" });
            context.QuestionResults.Add(new QuestionResult { Id = "questionResult1", AttemptId = "attempt1", QuestionId = "question2", Result = true });

            context.SaveChanges();
        }

        private static ServiceProvider GetInMemoryServiceProvider()
        {
            return new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
        }

        private static void RemoveShopDbContextRegistration(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<QuizDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }
    }
}
