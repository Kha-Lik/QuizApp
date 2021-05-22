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
    public class UserControllerTests
    {
        private Mock<IUserService> _userService;
        private UserController _userController;
        private UserDto _user;

        [SetUp]
        public void SetUp()
        {
            _userService = new Mock<IUserService>();
            _userController = new UserController(_userService.Object);

            _user = new UserDto { Id = "user1", Name = "Pavlo", Surname = "Pustovietov", Role = "Student"};
        }

        [Test]
        public void GetLecturers_ReturnsAllLecturers()
        {
            //Arrange
            _userService.Setup(s => s.GetUsersByRole("Lecturer")).Returns(new List<UserDto>().ToAsyncEnumerable());

            //Act
            var actionResult = _userController.GetAllLecturers();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<UserDto>>>();
        }

        [Test]
        public void GetStudents_ReturnsAllStudents()
        {
            //Arrange
            _userService.Setup(s => s.GetUsersByRole("Student")).Returns(new List<UserDto>().ToAsyncEnumerable());

            //Act
            var actionResult = _userController.GetAllStudents();


            //Assert 
            actionResult.Result.Should().BeOfType<ActionResult<IEnumerable<UserDto>>>();
        }

        [Test]
        public void GetByIdAndRole_ReturnsLecturer()
        {
            //Arrange
            _userService.Setup(s => s.GetUserByIdAndRole("user1", "Lecturer")).ReturnsAsync(_user);

            //Act
            var actionResult = _userController.GetLecturerById("user1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<UserDto>>();
        }

        [Test]
        public void GetByIdAndRole_ReturnsStudent()
        {
            //Arrange
            _userService.Setup(s => s.GetUserByIdAndRole("user1", "Student")).ReturnsAsync(_user);

            //Act
            var actionResult = _userController.GetStudentById("user1");

            //Assert
            actionResult.Result.Should().BeOfType<ActionResult<UserDto>>();
        }
    }
}
