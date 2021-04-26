using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Extensions;
using QuizApp.Business.Implementation.Exceptions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Implementation;

namespace QuizApp.Business.Implementation.Services
{
    public class TestService : ITestService
    {
        private readonly ICrudInterface<AttemptDto> _attemptService;
        private readonly ICrudInterface<TopicDto> _topicService;
        private readonly ICrudInterface<QuestionDto> _questionService;
        private readonly ICrudInterface<AnswerDto> _answerService;
        private readonly ICrudInterface<QuestionResultDto> _questionResultService;
        private readonly AbstractValidator<UserDto> _userValidator;
        private readonly AbstractValidator<TestModel> _testValidator;

        public TestService(
            ICrudInterface<AttemptDto> attemptService, 
            ICrudInterface<TopicDto> topicService,
            ICrudInterface<QuestionDto> questionService,
            ICrudInterface<AnswerDto> answerService,
            ICrudInterface<QuestionResultDto> questionResultService,
            AbstractValidator<TestModel> testValidator,
            AbstractValidator<UserDto> userValidator)
        {
            _attemptService = attemptService;
            _topicService = topicService;
            _questionService = questionService;
            _answerService = answerService;
            _questionResultService = questionResultService;
            _testValidator = testValidator;
            _userValidator = userValidator;
        }

        public async Task<IEnumerable<AttemptDto>> GetTestResultsForStudentAsync(UserDto student, string topicId)
        {
            await _userValidator.ValidateAsync(student);
            
            return  _attemptService.GetEntitiesByPrincipalId(student.Id)
                .Where(a => a.TopicId.Equals(topicId)).ToEnumerable();
        }

        public async Task<TestModel> GenerateTestForTopicAsync(UserDto student, string topicId)
        {
            await _userValidator.ValidateAsync(student);

            var topic = await _topicService.GetByIdAsync(topicId);
            
            var attemptsCount = (await GetTestResultsForStudentAsync(student, topicId)).Count();
            if (attemptsCount >= topic.MaxAttemptCount) 
                throw new TestCreationException("You have no available attempts");
            
            var topicQuestions = await _questionService.GetEntitiesByPrincipalId(topicId).ToListAsync();
            if (topicQuestions.Count < topic.QuestionsPerAttempt)
                throw new TestCreationException("Topic don't have enough questions for test");
            
            List<QuestionDto> testQuestions = new();

            while (testQuestions.Count < topic.QuestionsPerAttempt)
            {
                topicQuestions.Shuffle();
                var question = topicQuestions.First();
                
                if (testQuestions.Contains(question)) continue;
                
                var answers = await _answerService.GetEntitiesByPrincipalId(question.Id).ToListAsync();
                answers.ForEach(a => a.IsCorrect = false);
                answers.Shuffle();
                question.Answers = answers;
                
                testQuestions.Add(question);
            }

            TestModel test = new (){Student = student, Topic = topic, Questions = testQuestions};
            return test;
        }

        public async Task SubmitTestAsync(TestModel test)
        {
            await _testValidator.ValidateAsync(test);
            
            test.DateTimePassed = DateTime.Now;
            var score = 0;
            List<QuestionResultDto> results = new();

            foreach (var question in test.Questions)
            {
                var all = true;
                
                foreach (var answer in question.Answers)
                {
                    if (answer.IsCorrect.Equals((await _answerService.GetByIdAsync(answer.Id)).IsCorrect)) continue;
                    all = false;
                    break;
                }

                QuestionResultDto result = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    QuestionId = question.Id,
                    Result = false
                };
                
                if (all)
                {
                    result.Result = true;
                    score++;
                }
                
                results.Add(result);
            }

            test.Score = score;

            AttemptDto attemptDto = new()
            {
                Id = Guid.NewGuid().ToString(),
                StudentId = test.Student.Id,
                TopicId = test.Topic.Id,
                Score = test.Score,
                DateTime = test.DateTimePassed
            };
            
            results.ForEach(qr => qr.AttemptId = attemptDto.Id);

            await _attemptService.CreateEntityAsync(attemptDto);
            results.ForEach(async qr => await _questionResultService.CreateEntityAsync(qr));
        }
    }
}