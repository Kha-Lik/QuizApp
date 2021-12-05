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
    public class AttemptControllerTests
    {
        private Mock<ICrudInterface<AttemptDto>> _attemptService;
        private AttemptController _attemptController;
        private AttemptDto _attempt;

        [SetUp]
        public void SetUp()
        {
            _attemptService = new Mock<ICrudInterface<AttemptDto>>();
            _attemptController = new AttemptController(_attemptService.Object);

            _attempt = new AttemptDto { Id = "attempt1", DateTime = DateTime.Now, Score = 100, StudentId = "student1", TopicId = "topic1" };
        }

        [Test]
        public void GetAll_ReturnsAllAttempts()
        {
            //Arrange
            _attemptService.Setup(s => s.GetAllAsync()).Returns(new List<AttemptDto>().ToAsyncEnumerable());

            //Act
            var actionResult = _attemptController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<AttemptDto>>>();
        }

        [Test]
        public void GetById_ReturnsAttempt()
        {
            //Arrange
            _attemptService.Setup(s => s.GetByIdAsync("attempt1")).ReturnsAsync(_attempt);

            //Act
            var actionResult = _attemptController.GetById("attempt1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<AttemptDto>>();
        }

        [Test]
        public void Create_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _attemptService.Setup(s => s.CreateEntityAsync(_attempt));

            //Act
            var actionResult = _attemptController.Create(_attempt);

            //Assert
            actionResult.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public void Create_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _attemptService.Setup(s => s.CreateEntityAsync(_attempt)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _attemptController.Create(_attempt).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _attemptService.Setup(s => s.UpdateEntity(_attempt));

            //Act
            var actionResult = _attemptController.Update(_attempt).Result as OkResult;

            //Assert
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _attemptService.Setup(s => s.UpdateEntity(null)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _attemptController.Update(null).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _attemptService.Setup(s => s.DeleteEntityByIdAsync(_attempt.Id));

            //Act
            var actionResult = _attemptController.Delete(_attempt.Id).Result as OkResult;

            //Assert
            actionResult.StatusCode.Equals(200);
        }
    }
}
