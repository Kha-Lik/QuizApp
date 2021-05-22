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
    public class TopicController : ControllerBase
    {
        private readonly ICrudInterface<TopicDto> _topicService;
        public TopicController(ICrudInterface<TopicDto> topicService)
        {
            _topicService = topicService;
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Topics")]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetAll()
        {
            return await _topicService.GetAllAsync().ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDto>> GetById(string id)
        {
            return await _topicService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{SubjectId}")]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetBySubjectId(string id)
        {
            return await _topicService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost("Topic")]
        public async Task<ActionResult> Create([FromBody]TopicDto topicDto)
        {
            try
            {
                await _topicService.CreateEntityAsync(topicDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Create), new { topicDto.Id }, topicDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPut("Topic")]
        public async Task<ActionResult> Update(TopicDto topicDto)
        {
            await _topicService.UpdateEntity(topicDto);

            return Ok();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _topicService.DeleteEntityByIdAsync(id);

            return Ok();
        }
    }
}
