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
    public class QuestionResultControllerTests
    {
        private Mock<ICrudInterface<QuestionResultDto>> _questionResultService;
        private QuestionResultController _questionResultController;
        private QuestionResultDto _questionResult;

        [SetUp]
        public void SetUp()
        {
            _questionResultService = new Mock<ICrudInterface<QuestionResultDto>>();
            _questionResultController = new QuestionResultController(_questionResultService.Object);

            _questionResult = new QuestionResultDto { Id = "questionResult1", Result = true, AttemptId = "attempt1", QuestionId = "question1"};
        }

        [Test]
        public void GetAll_ReturnsAllQuestionResults()
        {
            //Arrange
            _questionResultService.Setup(s => s.GetAllAsync()).Returns(new List<QuestionResultDto>().ToAsyncEnumerable());

            //Act
            var actionResult = _questionResultController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<QuestionResultDto>>>();
        }

        [Test]
        public void GetById_ReturnsQuestionResult()
        {
            //Arrange
            _questionResultService.Setup(s => s.GetByIdAsync("questionResult1")).ReturnsAsync(_questionResult);

            //Act
            var actionResult = _questionResultController.GetById("questionResult1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<QuestionResultDto>>();
        }

        [Test]
        public void Create_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _questionResultService.Setup(s => s.CreateEntityAsync(_questionResult));

            //Act
            var actionResult = _questionResultController.Create(_questionResult);

            //Assert
            actionResult.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public void Create_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _questionResultService.Setup(s => s.CreateEntityAsync(_questionResult)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _questionResultController.Create(_questionResult).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _questionResultService.Setup(s => s.UpdateEntity(_questionResult));

            //Act
            var actionResult = _questionResultController.Update(_questionResult).Result as OkResult;

            //Assert
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _questionResultService.Setup(s => s.UpdateEntity(null)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _questionResultController.Update(null).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _questionResultService.Setup(s => s.DeleteEntityByIdAsync(_questionResult.Id));

            //Act
            var actionResult = _questionResultController.Delete(_questionResult.Id).Result as OkResult;

            //Assert
            actionResult.StatusCode.Equals(200);
        }
    }
}
