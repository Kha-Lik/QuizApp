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
using QuizApp.Business.Implementation.Exceptions;

namespace QuizApp.UnitTests.WebApiTests
{
    [TestFixture]
    public class TestControllerTests
    {
        private Mock<ITestService> _testService;
        private TestController _testController;
        private TestModel _test;
        private UserDto _user;

        [SetUp]
        public void SetUp()
        {
            _testService = new Mock<ITestService>();
            _testController = new TestController(_testService.Object);

            _test = new TestModel { DateTimePassed = DateTime.Now, Score = 50 };
            _user = new UserDto { Id = "user1", Name = "Pavel", Surname = "Pustovietov", Role = "Student"};
        }

        [Test]
        public void GetResults_ReturnsResults()
        {
            //Arrange
            _testService.Setup(s => s.GetTestResultsForStudentAsync(_user, "topic1")).ReturnsAsync(new List<AttemptDto>());

            //Act
            var actionResult = _testController.GetResultsAsync(_user, "topic1");


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<AttemptDto>>>();
        }

        [Test]
        public void GetTest_ReturnsTest()
        {
            //Arrange
            _testService.Setup(s => s.GenerateTestForTopicAsync(_user, "topic1")).ReturnsAsync(_test);

            //Act
            var actionResult = _testController.GetTestAsync(_user, "topic1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<TestModel>>();
        }

        [Test]
        public void GetTest_TestIsNotAvailable_ReturnsBadRequest()
        {
            //Arrange
            _testService.Setup(s => s.GenerateTestForTopicAsync(_user, "topic1")).ThrowsAsync(new TestCreationException());

            //Act
            var actionResult = _testController.GetTestAsync(_user, "topic1").Result;

            //Assert
            actionResult.Result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Test]
        public void SubmitTest_ReturnsOk()
        {
            //Arrange
            _testService.Setup(s => s.SubmitTestAsync(_test));

            //Act
            var actionResult = _testController.SubmitTestAsync(_test).Result as OkResult;

            //Assert
            actionResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void SubmitTest_InvalidTestModel_ReturnsBadRequest()
        {
            //Arrange
            _testService.Setup(s => s.SubmitTestAsync(_test)).ThrowsAsync(new Exception());

            //Act
            var actionResult = _testController.SubmitTestAsync(_test).Result as BadRequestObjectResult;

            //Assert
            actionResult.StatusCode.Should().Be(400);
        }
    }
}
