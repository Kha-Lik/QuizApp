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
    public class QuestionResultService : ICrudInterface<QuestionResultDto>
    {
        private readonly QuizDbContext _context;
        private readonly IServiceHelper<QuestionResult> _helper;
        private readonly AbstractValidator<QuestionResultDto> _validator;

        public QuestionResultService(QuizDbContext context, IServiceHelper<QuestionResult> helper, AbstractValidator<QuestionResultDto> validator)
        {
            _context = context;
            _helper = helper;
            _validator = validator;
        }

        public async Task CreateEntityAsync(QuestionResultDto model)
        {
            await _validator.ValidateAsync(model);
            await _context.QuestionResults.AddAsync(model.AdaptToQuestionResult());

            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<QuestionResultDto> GetEntitiesByPrincipalId(string principalId)
        {
            return _context.QuestionResults.Where(qr => qr.AttemptId.Equals(principalId))
                .Select(QuestionResultMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public IAsyncEnumerable<QuestionResultDto> GetAllAsync()
        {
            return _context.QuestionResults.Select(QuestionResultMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public async Task<QuestionResultDto> GetByIdAsync(string id)
        {
            var questionResult = await _context.QuestionResults.FindAsync(id);
            return questionResult.AdaptToDto();
        }

        public async Task UpdateEntity(QuestionResultDto model)
        {
            _helper.ThrowValidationExceptionIfModelIsNull(await _context.QuestionResults.FindAsync(model.Id));
            await _validator.ValidateAsync(model);

            _context.QuestionResults.Update(model.AdaptToQuestionResult());
        }

        public async Task DeleteEntityByIdAsync(string id)
        {
            var questionResult = await _context.QuestionResults.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(questionResult);

            _context.QuestionResults.Remove(questionResult);
            await _context.SaveChangesAsync();
        }
    }
}