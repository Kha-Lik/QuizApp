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
    public class AttemptController : ControllerBase
    {
        private readonly ICrudInterface<AttemptDto> _attemptService;
        public AttemptController(ICrudInterface<AttemptDto> attemptService)
        {
            _attemptService = attemptService;
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Attempts")]
        public async Task<ActionResult<IEnumerable<AttemptDto>>> GetAll()
        {
            return await _attemptService.GetAllAsync().ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AttemptDto>> GetById(string id)
        {
            return await _attemptService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{SubjectId}")]
        public async Task<ActionResult<IEnumerable<AttemptDto>>> GetBySubjectId(string id)
        {
            return await _attemptService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost("Attempt")]
        public async Task<ActionResult> Create([FromBody]AttemptDto attemptDto)
        {
            try
            {
                await _attemptService.CreateEntityAsync(attemptDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Create), new { attemptDto.Id }, attemptDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPut("Attempt")]
        public async Task<ActionResult> Update(AttemptDto attemptDto)
        {
            try
            {
                await _attemptService.UpdateEntity(attemptDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _attemptService.DeleteEntityByIdAsync(id);

            return Ok();
        }
    }
}
