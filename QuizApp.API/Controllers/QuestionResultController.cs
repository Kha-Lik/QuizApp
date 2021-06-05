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
    public class QuestionResultController : ControllerBase
    {
        private readonly ICrudInterface<QuestionResultDto> _questionResultService;
        public QuestionResultController(ICrudInterface<QuestionResultDto> questionResultService)
        {
            _questionResultService = questionResultService;
        }

        //[Authorize(Roles = "Lecturer, Student")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionResultDto>>> GetAll()
        {
            return await _questionResultService.GetAllAsync().ToListAsync();
        }

        //[Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionResultDto>> GetById(string id)
        {
            return await _questionResultService.GetByIdAsync(id);
        }

        //[Authorize(Roles = "Lecturer, Student")]
        [HttpGet("attempt={id}")]
        public async Task<ActionResult<IEnumerable<QuestionResultDto>>> GetByQuestionId(string id)
        {
            return await _questionResultService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        //[Authorize(Roles = "Lecturer")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]QuestionResultDto questionResultDto)
        {
            try
            {
                await _questionResultService.CreateEntityAsync(questionResultDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Create), new { questionResultDto.Id }, questionResultDto);
        }

        //[Authorize(Roles = "Lecturer")]
        [HttpPut]
        public async Task<ActionResult> Update(QuestionResultDto questionResultDto)
        {
            try
            {
                await _questionResultService.UpdateEntity(questionResultDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        //[Authorize(Roles = "Lecturer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _questionResultService.DeleteEntityByIdAsync(id);

            return Ok();
        }
    }
}
