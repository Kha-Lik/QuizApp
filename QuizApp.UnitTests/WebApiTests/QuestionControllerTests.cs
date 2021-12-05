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
    public class QuestionControllerTests
    {
        private Mock<ICrudInterface<QuestionDto>> _questionService;
        private QuestionController _questionController;
        private QuestionDto _question;

        [SetUp]
        public void SetUp()
        {
            _questionService = new Mock<ICrudInterface<QuestionDto>>();
            _questionController = new QuestionController(_questionService.Object);

            _question = new QuestionDto { Id = "question1", QuestionNumber = 5, QuestionText = "How dare you?", TopicId = "topic1" };
        }

        [Test]
        public void GetAll_ReturnsAllQuestions()
        {
            //Arrange
            _questionService.Setup(s => s.GetAllAsync()).Returns(new List<QuestionDto>().ToAsyncEnumerable());

            //Act
            var actionResult = _questionController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<QuestionDto>>>();
        }

        [Test]
        public void GetById_ReturnsQuestion()
        {
            //Arrange
            _questionService.Setup(s => s.GetByIdAsync("question1")).ReturnsAsync(_question);

            //Act
            var actionResult = _questionController.GetById("question1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<QuestionDto>>();
        }

        [Test]
        public void Create_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _questionService.Setup(s => s.CreateEntityAsync(_question));

            //Act
            var actionResult = _questionController.Create(_question);

            //Assert
            actionResult.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public void Create_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _questionService.Setup(s => s.CreateEntityAsync(_question)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _questionController.Create(_question).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _questionService.Setup(s => s.UpdateEntity(_question));

            //Act
            var actionResult = _questionController.Update(_question).Result as OkResult;

            //Assert
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _questionService.Setup(s => s.UpdateEntity(null)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _questionController.Update(null).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _questionService.Setup(s => s.DeleteEntityByIdAsync(_question.Id));

            //Act
            var actionResult = _questionController.Delete(_question.Id).Result as OkResult;

            //Assert
            actionResult.StatusCode.Equals(200);
        }
    }
}
