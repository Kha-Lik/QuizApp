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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetAll()
        {
            return await _topicService.GetAllAsync().ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Get by id = {id}")]
        public async Task<ActionResult<TopicDto>> GetById(string id)
        {
            return await _topicService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("Get by subject id = {id}")]
        public async Task<ActionResult<IEnumerable<TopicDto>>> GetBySubjectId(string id)
        {
            return await _topicService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost]
        public async Task<ActionResult<TopicDto>> Create(TopicDto topicDto)
        {
            try
            {
                await _topicService.CreateEntityAsync(topicDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(topicDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPut]
        public async Task<ActionResult<TopicDto>> Update(TopicDto topicDto)
        {
            await _topicService.UpdateEntity(topicDto);

            return Ok(topicDto);
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
