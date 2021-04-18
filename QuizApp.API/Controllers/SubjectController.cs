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
    public class SubjectController : Controller
    {

        private readonly ICrudInterface<SubjectDto> _subjectService;
        public SubjectController(ICrudInterface<SubjectDto> subjectService)
        {
            _subjectService = subjectService;
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetAll()
        {
            return await _subjectService.GetAllAsync().ToListAsync();
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<SubjectDto>> GetById(string id)
        {
            return await _subjectService.GetByIdAsync(id);
        }

        [Authorize(Roles = "Lecturer, Student")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SubjectDto>>> GetByLecturerId(string id)
        {
            return await _subjectService.GetEntitiesByPrincipalId(id).ToListAsync();
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPost]
        public async Task<ActionResult<SubjectDto>> Create(SubjectDto subjectDto)
        {
            try
            {
                await _subjectService.CreateEntityAsync(subjectDto);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(subjectDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpPut]
        public async Task<ActionResult<SubjectDto>> Update(SubjectDto subjectDto)
        {
            await _subjectService.UpdateEntity(subjectDto);

            return Ok(subjectDto);
        }

        [Authorize(Roles = "Lecturer")]
        [HttpDelete]
        public async Task<ActionResult> Delete(string id)
        {
            await _subjectService.DeleteEntityByIdAsync(id);

            return Ok();
        }
    }
}
