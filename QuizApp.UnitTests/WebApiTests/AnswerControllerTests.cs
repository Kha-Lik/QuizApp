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
    public class AnswerControllerTests
    {
        private Mock<ICrudInterface<AnswerDto>> _answerService;
        private AnswerController _answerController;
        private AnswerDto _answer;

        [SetUp]
        public void SetUp()
        {
            _answerService = new Mock<ICrudInterface<AnswerDto>>();
            _answerController = new AnswerController(_answerService.Object);

            _answer = new AnswerDto { Id = "answer1", AnswerText = "Hello", IsCorrect = true, QuestionId = "question1" };
        }

        [Test]
        public void GetAll_ReturnsAllAnswers()
        {
            //Arrange
            _answerService.Setup(s => s.GetAllAsync()).Returns(new List<AnswerDto>().ToAsyncEnumerable());

            //Act
            var actionResult = _answerController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<AnswerDto>>>();
        }

        [Test]
        public void GetById_ReturnsAnswer()
        {
            //Arrange
            _answerService.Setup(s => s.GetByIdAsync("answer1")).ReturnsAsync(_answer);

            //Act
            var actionResult = _answerController.GetById("answer1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<AnswerDto>>();
        }

        [Test]
        public void Create_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _answerService.Setup(s => s.CreateEntityAsync(_answer));

            //Act
            var actionResult = _answerController.Create(_answer);

            //Assert
            actionResult.Result.Should().BeOfType<CreatedAtActionResult>();
        }
        
        [Test]
        public void Create_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _answerService.Setup(s => s.CreateEntityAsync(_answer)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _answerController.Create(_answer).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }
        
        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _answerService.Setup(s => s.UpdateEntity(_answer));

            //Act
            var actionResult = _answerController.Update(_answer).Result as OkResult;

            //Assert
            actionResult.StatusCode.Should().Be(200);
        }
        
        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _answerService.Setup(s => s.UpdateEntity(null)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _answerController.Update(null).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }
        
        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _answerService.Setup(s => s.DeleteEntityByIdAsync(_answer.Id));

            //Act
            var actionResult = _answerController.Delete(_answer.Id).Result as OkResult;

            //Assert
            actionResult.StatusCode.Equals(200);
        }
    }
}
