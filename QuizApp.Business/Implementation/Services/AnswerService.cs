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
    public class AnswerService : ICrudInterface<AnswerDto>
    {
        private readonly QuizDbContext _context;
        private readonly IServiceHelper<Answer> _helper;
        private readonly AbstractValidator<AnswerDto> _validator;

        public AnswerService(QuizDbContext context, IServiceHelper<Answer> helper, AbstractValidator<AnswerDto> validator)
        {
            _context = context;
            _helper = helper;
            _validator = validator;
        }

        public async Task CreateEntityAsync(AnswerDto model)
        {
            await _validator.ValidateAsync(model);
            await _context.Answers.AddAsync(model.AdaptToAnswer());

            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<AnswerDto> GetEntitiesByPrincipalId(string principalId)
        {
            return _context.Answers.AsQueryable().Where(a => a.QuestionId.Equals(principalId)).Select(AnswerMapper.ProjectToDto)
                .AsAsyncEnumerable();
        }

        public IAsyncEnumerable<AnswerDto> GetAllAsync()
        {
            return _context.Answers.Select(AnswerMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public async Task<AnswerDto> GetByIdAsync(string id)
        {
            var answer = await _context.Answers.FindAsync(id);
            return answer.AdaptToDto();
        }

        public async Task UpdateEntity(AnswerDto model)
        {
            _helper.ThrowValidationExceptionIfModelIsNull(await _context.Answers.FindAsync(model.Id));
            await _validator.ValidateAsync(model);

            _context.Answers.Update(model.AdaptToAnswer());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(string id)
        {
            var answer = await _context.Answers.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(answer);

            _context.Answers.Remove(answer);
            await _context.SaveChangesAsync();
        }
    }
}