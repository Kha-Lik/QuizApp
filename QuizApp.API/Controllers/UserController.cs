using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Lecturers")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllLecturers()
        {
            return await _userService.GetUsersByRole("Lecturer").ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Students")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllStudents()
        {
            return await _userService.GetUsersByRole("Student").ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{LecturerId}")]
        public async Task<ActionResult<UserDto>> GetLecturerById(string id)
        {
            return await _userService.GetUserByIdAndRole(id, "Lecturer");
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{StudentId}")]
        public async Task<ActionResult<UserDto>> GetStudentById(string id)
        {
            return await _userService.GetUserByIdAndRole(id, "Student");
        }

    }
}
