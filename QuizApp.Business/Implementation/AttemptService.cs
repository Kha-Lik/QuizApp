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

namespace QuizApp.Business.Implementation
{
    public class AttemptService : ICrudInterface<AttemptDto>
    {
        private readonly QuizDbContext _context;
        private readonly IServiceHelper<Attempt> _helper;
        //TODO: write validator for this model
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
            return _context.Attempts.Where(a => a.StudentId.Equals(principalId)).Select(AttemptMapper.ProjectToDto).AsAsyncEnumerable();
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
            _helper.ThrowValidationExceptionIfModelIsNull(await _context.Attempts.FindAsync(model.Id));
            await _validator.ValidateAsync(model);

            _context.Attempts.Update(model.AdaptToAttempt());
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