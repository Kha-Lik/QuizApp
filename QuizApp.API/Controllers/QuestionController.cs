using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class QuestionController : ControllerBase
    {
        private readonly ICrudInterface<QuestionDto> _questionService;
        public QuestionController(ICrudInterface<QuestionDto> questionService)
        {
            _questionService = questionService;
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Questions")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAll()
        {
            return await _questionService.GetAllAsync().ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDto>> GetById(string id)
        {
            return await _questionService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{LecturerId}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetByLecturerId(string id)
        {
            return await _questionService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost("Question")]
        public async Task<ActionResult> Create([FromBody] QuestionDto questionDto)
        {
            try
            {
                await _questionService.CreateEntityAsync(questionDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Create), new { questionDto.Id }, questionDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPut("Question")]
        public async Task<ActionResult> Update(QuestionDto questionDto)
        {
            await _questionService.UpdateEntity(questionDto);

            return Ok();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _questionService.DeleteEntityByIdAsync(id);

            return Ok();
        }
    }
}
