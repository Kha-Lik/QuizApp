using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
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
    public class AnswerController : ControllerBase
    {
        private readonly ICrudInterface<AnswerDto> _answerService;
        public AnswerController(ICrudInterface<AnswerDto> answerService)
        {
            _answerService = answerService;
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Answers")]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAll()
        {
            return await _answerService.GetAllAsync().ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswerDto>> GetById(string id)
        {
            return await _answerService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{SubjectId}")]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetBySubjectId(string id)
        {
            return await _answerService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost("Answer")]
        public async Task<ActionResult> Create([FromBody]AnswerDto answerDto)
        {
            try
            {
                await _answerService.CreateEntityAsync(answerDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Create), new { answerDto.Id }, answerDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPut("Answer")]
        public async Task<ActionResult> Update(AnswerDto answerDto)
        {
            await _answerService.UpdateEntity(answerDto);

            return Ok();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _answerService.DeleteEntityByIdAsync(id);

            return Ok();
        }
    }
}
