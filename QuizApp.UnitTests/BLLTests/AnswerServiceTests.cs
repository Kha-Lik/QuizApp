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
    public class AnswerServiceTests
    {
        private Mock<AbstractValidator<AnswerDto>> _validator;
        private Mock<IServiceHelper<Answer>> _helper;
        private AnswerService _service;
        private QuizDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<AnswerDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<AnswerDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new QuizDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<Answer>>();
            _service = new AnswerService(_context, _helper.Object, _validator.Object );
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Answers.AsQueryable().CountAsync();
            var result = _service.GetAllAsync().ToListAsync();

            result.Result.Count().Should().Be(numberOfItemsInDatabase);
        }
        
        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsAnswerDto()
        {
            var actual = await _service.GetByIdAsync("answer1");

            actual.Should().BeOfType<AnswerDto>();
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
            var numberOfItemsInDatabase = await _context.Answers.AsQueryable().CountAsync();
            await _service.CreateEntityAsync(new AnswerDto { AnswerText = "testanswer", IsCorrect = false, QuestionId = "question1", Id = "testAnswer1" });

            _context.Answers.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }
        
        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Answers.AsQueryable().CountAsync();
            await _service.DeleteEntityByIdAsync("answer2");

            _context.Answers.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Answers.FindAsync("answer2").Result.Should().BeNull();
        }
        
        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var answerBeforeUpdate = await _context.Answers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "answer1");

            await _service.UpdateEntity(new AnswerDto { Id = "answer1", AnswerText = "x = 100", IsCorrect = true, QuestionId = "question1" });

            var answerAfterUpdate = await _context.Answers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "answer1");
            answerAfterUpdate.Id.Should().Be(answerBeforeUpdate.Id);
            answerAfterUpdate.AnswerText.Should().NotBe(answerBeforeUpdate.AnswerText);
            _validator.VerifyAll();
        }
    }
}
