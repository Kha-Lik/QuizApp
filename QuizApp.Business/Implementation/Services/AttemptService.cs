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
    public class AttemptService : ICrudInterface<AttemptDto>
    {
        private readonly QuizDbContext _context;
        private readonly IServiceHelper<Attempt> _helper;
        private readonly AbstractValidator<AttemptDto> _validator;

        public AttemptService(QuizDbContext context, IServiceHelper<Attempt> helper, AbstractValidator<AttemptDto> validator)
        {
            _context = context;
            _helper = helper;
            _validator = validator;
        }

        public async Task CreateEntityAsync(AttemptDto model)
        {
            await _validator.ValidateAsync(model);
            await _context.Attempts.AddAsync(model.AdaptToAttempt());

            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<AttemptDto> GetEntitiesByPrincipalId(string principalId)
        {
            return _context.Attempts.AsQueryable().Where(a => a.StudentId.Equals(principalId)).Select(AttemptMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public IAsyncEnumerable<AttemptDto> GetAllAsync()
        {
            return _context.Attempts.Select(AttemptMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public async Task<AttemptDto> GetByIdAsync(string id)
        {
            var attempt = await _context.Attempts.FindAsync(id);
            return attempt.AdaptToDto();
        }

        public async Task UpdateEntity(AttemptDto model)
        {
            var entity = await _context.Attempts.FindAsync(model.Id);
            _helper.ThrowValidationExceptionIfModelIsNull(entity);
            await _validator.ValidateAsync(model);

            entity = model.AdaptTo(entity);

            _context.Attempts.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(string id)
        {
            var attempt = await _context.Attempts.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(attempt);

            _context.Attempts.Remove(attempt);
            await _context.SaveChangesAsync();
        }
    }
}