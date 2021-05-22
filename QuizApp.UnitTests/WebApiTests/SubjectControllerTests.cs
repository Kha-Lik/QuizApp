using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuizApp.API.Controllers;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Models;
using QuizApp.Business.Implementation.Services;

namespace QuizApp.UnitTests.WebApiTests
{
    [TestFixture]
    public class SubjectControllerTests
    {
        private Mock<ICrudInterface<SubjectDto>> _subjectService;
        private SubjectController _subjectController;
        private SubjectDto _subject;

        [SetUp]
        public void SetUp()
        {
            _subjectService = new Mock<ICrudInterface<SubjectDto>>();
            _subjectController = new SubjectController(_subjectService.Object);

            _subject = new SubjectDto { Id = "subject1", Name = "TRPZ", LecturerId = "lecturer1" };
        }

        [Test]
        public void GetAll_ReturnsAllSubjects()
        {
            //Arrange
            _subjectService.Setup(s => s.GetAllAsync()).Returns(new List<SubjectDto>().ToAsyncEnumerable());

            //Act
            var actionResult = _subjectController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<SubjectDto>>>();
        }

        [Test]
        public void GetById_ReturnsSubject()
        {
            //Arrange
            _subjectService.Setup(s => s.GetByIdAsync("subject1")).ReturnsAsync(_subject);

            //Act
            var actionResult = _subjectController.GetById("subject1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<SubjectDto>>();
        }

        [Test]
        public void Create_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _subjectService.Setup(s => s.CreateEntityAsync(_subject));

            //Act
            var actionResult = _subjectController.Create(_subject);

            //Assert
            actionResult.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public void Create_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _subjectService.Setup(s => s.CreateEntityAsync(_subject)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _subjectController.Create(_subject).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _subjectService.Setup(s => s.UpdateEntity(_subject));

            //Act
            var actionResult = _subjectController.Update(_subject).Result as OkResult;

            //Assert
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _subjectService.Setup(s => s.UpdateEntity(null)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _subjectController.Update(null).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _subjectService.Setup(s => s.DeleteEntityByIdAsync(_subject.Id));

            //Act
            var actionResult = _subjectController.Delete(_subject.Id).Result as OkResult;

            //Assert
            actionResult.StatusCode.Equals(200);
        }
    }
}
