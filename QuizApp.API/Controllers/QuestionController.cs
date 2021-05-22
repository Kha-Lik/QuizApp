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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetAll()
        {
            return await _questionService.GetAllAsync().ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Get by id = {id}")]
        public async Task<ActionResult<QuestionDto>> GetById(string id)
        {
            return await _questionService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Get by lecturer id = {id}")]
        public async Task<ActionResult<IEnumerable<QuestionDto>>> GetByLecturerId(string id)
        {
            return await _questionService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost]
        public async Task<ActionResult<QuestionDto>> Create(QuestionDto questionDto)
        {
            try
            {
                await _questionService.CreateEntityAsync(questionDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(questionDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPut]
        public async Task<ActionResult<QuestionDto>> Update(QuestionDto questionDto)
        {
            await _questionService.UpdateEntity(questionDto);

            return Ok(questionDto);
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
