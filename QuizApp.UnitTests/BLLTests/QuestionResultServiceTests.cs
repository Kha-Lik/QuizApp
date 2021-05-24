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
    public class QuestionResultServiceTests
    {
        private Mock<AbstractValidator<QuestionResultDto>> _validator;
        private Mock<IServiceHelper<QuestionResult>> _helper;
        private QuestionResultService _service;
        private QuizDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<QuestionResultDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<QuestionResultDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new QuizDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<QuestionResult>>();
            _service = new QuestionResultService(_context, _helper.Object, _validator.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.QuestionResults.AsQueryable().CountAsync();
            var result = _service.GetAllAsync().ToListAsync();

            result.Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsQuestionResultDto()
        {
            var actual = await _service.GetByIdAsync("questionResult1");

            actual.Should().BeOfType<QuestionResultDto>();
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
            var numberOfItemsInDatabase = await _context.QuestionResults.AsQueryable().CountAsync();
            await _service.CreateEntityAsync(new QuestionResultDto { Id = "testQuestionResult", Result = true, AttemptId = "attempt1", QuestionId = "question2" });

            _context.QuestionResults.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }

        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.QuestionResults.AsQueryable().CountAsync();
            await _service.DeleteEntityByIdAsync("questionResult1");

            _context.QuestionResults.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.QuestionResults.FindAsync("questionResult1").Result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var questionResultBeforeUpdate = await _context.QuestionResults.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "questionResult1");

            await _service.UpdateEntity(new QuestionResultDto { Id = "questionResult1", Result = false, AttemptId = "attempt1", QuestionId = "question2" });

            var questionResultAfterUpdate = await _context.QuestionResults.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "questionResult1");
            questionResultAfterUpdate.Id.Should().Be(questionResultBeforeUpdate.Id);
            questionResultAfterUpdate.Result.Should().Be(!questionResultBeforeUpdate.Result);
            _validator.VerifyAll();
        }
    }
}
