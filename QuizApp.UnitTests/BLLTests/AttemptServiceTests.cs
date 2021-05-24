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
    public class AttemptServiceTests
    {
        private Mock<AbstractValidator<AttemptDto>> _validator;
        private Mock<IServiceHelper<Attempt>> _helper;
        private AttemptService _service;
        private QuizDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<AttemptDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<AttemptDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new QuizDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<Attempt>>();
            _service = new AttemptService(_context, _helper.Object, _validator.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Attempts.AsQueryable().CountAsync();
            var result = _service.GetAllAsync().ToListAsync();

            result.Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsAttemptDto()
        {
            var actual = await _service.GetByIdAsync("attempt1");

            actual.Should().BeOfType<AttemptDto>();
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
            var numberOfItemsInDatabase = await _context.Attempts.AsQueryable().CountAsync();
            await _service.CreateEntityAsync(new AttemptDto { Id = "testAttempt", DateTime = DateTime.Now, Score = 58, StudentId = "student1", TopicId = "topic2" });

            _context.Attempts.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }

        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Attempts.AsQueryable().CountAsync();
            await _service.DeleteEntityByIdAsync("attempt1");

            _context.Attempts.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Attempts.FindAsync("attempt1").Result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var attemptBeforeUpdate = await _context.Attempts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "attempt1");

            await _service.UpdateEntity(new AttemptDto { Id = "attempt1", DateTime = DateTime.Now, Score = 58, StudentId = "1", TopicId = "topic1" });

            var attemptAfterUpdate = await _context.Attempts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "attempt1");
            attemptAfterUpdate.Id.Should().Be(attemptBeforeUpdate.Id);
            attemptAfterUpdate.Score.Should().NotBe(attemptBeforeUpdate.Score);
            _validator.VerifyAll();
        }
    }
}
