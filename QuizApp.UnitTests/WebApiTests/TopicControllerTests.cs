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
    public class TopicControllerTests
    {
        private Mock<ICrudInterface<TopicDto>> _topicService;
        private TopicController _topicController;
        private TopicDto _topic;

        [SetUp]
        public void SetUp()
        {
            _topicService = new Mock<ICrudInterface<TopicDto>>();
            _topicController = new TopicController(_topicService.Object);

            _topic = new TopicDto { Id = "topic1", Name = "Unit Tests", MaxAttemptCount = 3, QuestionsPerAttempt = 5, TimeToPass = 5, TopicNumber = 5, SubjectId = "subject1" };
        }

        [Test]
        public void GetAll_ReturnsAllTopics()
        {
            //Arrange
            _topicService.Setup(s => s.GetAllAsync()).Returns(new List<TopicDto>().ToAsyncEnumerable());

            //Act
            var actionResult = _topicController.GetAll();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<TopicDto>>>();
        }

        [Test]
        public void GetById_ReturnsTopic()
        {
            //Arrange
            _topicService.Setup(s => s.GetByIdAsync("topic1")).ReturnsAsync(_topic);

            //Act
            var actionResult = _topicController.GetById("topic1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<TopicDto>>();
        }

        [Test]
        public void Create_ModelIsValid_ReturnsCreatedAtAction()
        {
            //Arrange
            _topicService.Setup(s => s.CreateEntityAsync(_topic));

            //Act
            var actionResult = _topicController.Create(_topic);

            //Assert
            actionResult.Result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Test]
        public void Create_ModelIsInvalid_ReturnsBadRequest()
        {
            //Arrange
            _topicService.Setup(s => s.CreateEntityAsync(_topic)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _topicController.Create(_topic).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Update_ModelIsValid_ReturnsOK()
        {
            //Arrange
            _topicService.Setup(s => s.UpdateEntity(_topic));

            //Act
            var actionResult = _topicController.Update(_topic).Result as OkResult;

            //Assert
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void Update_ModelIsNotValid_ReturnsBadRequest()
        {
            //Arrange
            _topicService.Setup(s => s.UpdateEntity(null)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _topicController.Update(null).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }

        [Test]
        public void Delete_ReturnsOk()
        {
            //Arrange
            _topicService.Setup(s => s.DeleteEntityByIdAsync(_topic.Id));

            //Act
            var actionResult = _topicController.Delete(_topic.Id).Result as OkResult;

            //Assert
            actionResult.StatusCode.Equals(200);
        }
    }
}
