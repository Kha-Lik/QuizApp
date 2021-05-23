using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Implementation.Exceptions;
using QuizApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpPost("Results")]
        public async Task<ActionResult<IEnumerable<AttemptDto>>> GetResultsAsync([FromBody]UserDto user, string topicId)
        {
            var result = await _testService.GetTestResultsForStudentAsync(user, topicId);
            return Ok(result);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpPost("CreateTest")]
        public async Task<ActionResult<TestModel>> GetTestAsync([FromBody]UserDto user, string topicId)
        {
            try
            {
                var result = await _testService.GenerateTestForTopicAsync(user, topicId);
                return Ok(result);
            }
            catch(TestCreationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpPost("Submit")]
        public async Task<ActionResult> SubmitTestAsync(TestModel test)
        {
            try
            {
                await _testService.SubmitTestAsync(test);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
