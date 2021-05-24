using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;
using FluentValidation.Results;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Implementation.Services;
using QuizApp.Business.Implementation.Validation;
using QuizApp.Business.Extensions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Entities;
using QuizApp.DataAccess.Implementation;
using System.Threading;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.UnitTests.BLLTests
{
    [TestFixture]
    public class QuestionServiceTests
    {
        private Mock<AbstractValidator<QuestionDto>> _validator;
        private Mock<IServiceHelper<Question>> _helper;
        private QuestionService _service;
        private QuizDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<QuestionDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<QuestionDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new QuizDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<Question>>();
            _service = new QuestionService(_context, _helper.Object, _validator.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Questions.AsQueryable().CountAsync();
            var result = _service.GetAllAsync().ToListAsync();

            result.Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsQuestionDto()
        {
            var actual = await _service.GetByIdAsync("question1");

            actual.Should().BeOfType<QuestionDto>();
        }

        [Test]
        public async Task GetByIdAsync_ItemDoesntExist_ReturnsNull()
        {
            var actual = await _service.GetByIdAsync("null");

            actual.Should().BeNull();
        }

        [Test]
        public async Task AddAsync_AddsItemToDatabase()
        {
            var numberOfItemsInDatabase = await _context.Questions.AsQueryable().CountAsync();
            await _service.CreateEntityAsync(new QuestionDto { Id = "testQuestion1", QuestionNumber = 10, QuestionText = "Why are you gay?", TopicId = "topic1" });

            _context.Questions.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }

        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Questions.AsQueryable().CountAsync();
            await _service.DeleteEntityByIdAsync("question2");

            _context.Questions.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Questions.FindAsync("question2").Result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var questionBeforeUpdate = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "question1");

            await _service.UpdateEntity(new QuestionDto { Id = "question1", QuestionText = "x = 100?", QuestionNumber = 5, TopicId = "topic1" });

            var questionAfterUpdate = await _context.Questions.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "question1");
            questionAfterUpdate.Id.Should().Be(questionBeforeUpdate.Id);
            questionAfterUpdate.QuestionText.Should().NotBe(questionBeforeUpdate.QuestionText);
            _validator.VerifyAll();
        }
    }
}

