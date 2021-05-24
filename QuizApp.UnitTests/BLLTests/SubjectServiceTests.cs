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
    public class SubjectServiceTests
    {
        private Mock<AbstractValidator<SubjectDto>> _validator;
        private Mock<IServiceHelper<Subject>> _helper;
        private SubjectService _service;
        private QuizDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<SubjectDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<SubjectDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new QuizDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<Subject>>();
            _service = new SubjectService(_context, _helper.Object, _validator.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Subjects.AsQueryable().CountAsync();
            var result = _service.GetAllAsync().ToListAsync();

            result.Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsSubjectDto()
        {
            var actual = await _service.GetByIdAsync("subject1");

            actual.Should().BeOfType<SubjectDto>();
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
            var numberOfItemsInDatabase = await _context.Subjects.AsQueryable().CountAsync();
            await _service.CreateEntityAsync(new SubjectDto { Id = "testSubject1", Name = "Test Subject", LecturerId = "1" });

            _context.Subjects.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }

        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Subjects.AsQueryable().CountAsync();
            await _service.DeleteEntityByIdAsync("subject2");

            _context.Subjects.AsQueryable().CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Subjects.FindAsync("subject2").Result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var subjectBeforeUpdate = await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "subject1");

            await _service.UpdateEntity(new SubjectDto { Id = "subject1", Name = "Another subject", LecturerId = "1" });

            var subjectAfterUpdate = await _context.Subjects.AsNoTracking().FirstOrDefaultAsync(c => c.Id == "subject1");
            subjectAfterUpdate.Id.Should().Be(subjectBeforeUpdate.Id);
            subjectAfterUpdate.Name.Should().NotBe(subjectBeforeUpdate.Name);
            _validator.VerifyAll();
        }
    }
}
