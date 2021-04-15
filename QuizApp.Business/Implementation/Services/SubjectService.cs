using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using QuizApp.Business.Abstraction;
using QuizApp.Business.Extensions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Entities;
using QuizApp.DataAccess.Implementation;

namespace QuizApp.Business.Implementation.Services
{
    public class SubjectService : ICrudInterface<SubjectDto>
    {
        private readonly QuizDbContext _context;
        private readonly IServiceHelper<Subject> _helper;
        private readonly AbstractValidator<SubjectDto> _validator;

        public SubjectService(QuizDbContext context, IServiceHelper<Subject> helper, AbstractValidator<SubjectDto> validator)
        {
            _context = context;
            _helper = helper;
            _validator = validator;
        }

        public async Task CreateEntityAsync(SubjectDto model)
        {
            await _validator.ValidateAsync(model);
            await _context.Subjects.AddAsync(model.AdaptToSubject());

            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<SubjectDto> GetEntitiesByPrincipalId(string principalId)
        {
            return _context.Subjects.Where(s => s.LecturerId.Equals(principalId)).Select(SubjectMapper.ProjectToDto)
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<SubjectDto> GetAllAsync()
        {
            return _context.Subjects.Select(SubjectMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public async Task<SubjectDto> GetByIdAsync(string id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            return subject.AdaptToDto();
        }

        public async Task UpdateEntity(SubjectDto model)
        {
            _helper.ThrowValidationExceptionIfModelIsNull(await _context.Subjects.FindAsync(model.Id));
            await _validator.ValidateAsync(model);

            _context.Subjects.Update(model.AdaptToSubject());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(string id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(subject);

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }
}