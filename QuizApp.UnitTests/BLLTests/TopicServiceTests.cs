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
    public class TopicServiceTests
    {
        private Mock<AbstractValidator<TopicDto>> _validator;
        private Mock<IServiceHelper<Topic>> _helper;
        private TopicService _service;
        private QuizDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<TopicDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<TopicDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new QuizDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<Topic>>();
            _service = new TopicService(_context, _helper.Object, _validator.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Topics.AsQueryable().CountAsync();
            var result = _service.GetAllAsync().ToListAsync();

            result.Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsTopicDto()
        {
            var actual = await _service.GetByIdAsync("topic1");

            actual.Should().BeOfType<TopicDto>();
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
            var numberOfItemsInDatabase = await _context.Topics.AsQueryable().CountAsync();
            await _service.CreateEntityAsync(new TopicDto { Id = "testTopic1", Name = "Test Topic", MaxAttemptCount = 2, 
                QuestionsPerAttempt = 17, TimeToPass = 25, TopicNumber = 5, SubjectId = "subject2" });

            _context.Topics.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }

        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Topics.AsQueryable().CountAsync();
            await _service.DeleteEntityByIdAsync("topic2");

            _context.Topics.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Topics.FindAsync("topic2").Result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var topicBeforeUpdate = await _context.Topics.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "topic1");

            await _service.UpdateEntity(new TopicDto { Id = "topic1",
                Name = "Another Topic",
                MaxAttemptCount = 2,
                QuestionsPerAttempt = 17,
                TimeToPass = 25,
                TopicNumber = 5,
                SubjectId = "subject2"
            });

            var topicAfterUpdate = await _context.Topics.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "topic1");
            topicAfterUpdate.Id.Should().Be(topicBeforeUpdate.Id);
            topicAfterUpdate.Name.Should().NotBe(topicBeforeUpdate.Name);
            _validator.VerifyAll();
        }
    }
}
