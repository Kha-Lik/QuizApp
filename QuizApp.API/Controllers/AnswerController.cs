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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetAll()
        {
            return await _answerService.GetAllAsync().ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Get by id = {id}")]
        public async Task<ActionResult<AnswerDto>> GetById(string id)
        {
            return await _answerService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Get by subject id = {id}")]
        public async Task<ActionResult<IEnumerable<AnswerDto>>> GetBySubjectId(string id)
        {
            return await _answerService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost]
        public async Task<ActionResult<AnswerDto>> Create(AnswerDto answerDto)
        {
            try
            {
                await _answerService.CreateEntityAsync(answerDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(answerDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPut]
        public async Task<ActionResult<AnswerDto>> Update(AnswerDto answerDto)
        {
            await _answerService.UpdateEntity(answerDto);

            return Ok(answerDto);
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
