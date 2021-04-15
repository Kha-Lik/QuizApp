using System.Collections.Generic;
using System.IO;
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
    public class QuestionService : ICrudInterface<QuestionDto>
    {
        private readonly QuizDbContext _context;
        private readonly IServiceHelper<Question> _helper;
        private readonly AbstractValidator<QuestionDto> _validator;

        public QuestionService(QuizDbContext context, IServiceHelper<Question> helper, AbstractValidator<QuestionDto> validator)
        {
            _context = context;
            _helper = helper;
            _validator = validator;
        }

        public async Task CreateEntityAsync(QuestionDto model)
        {
            await _validator.ValidateAsync(model);
            await _context.Questions.AddAsync(model.AdaptToQuestion());

            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<QuestionDto> GetEntitiesByPrincipalId(string principalId)
        {
            return _context.Questions.Where(q => q.TopicId.Equals(principalId)).Select(QuestionMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public IAsyncEnumerable<QuestionDto> GetAllAsync()
        {
            return _context.Questions.Select(QuestionMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public async Task<QuestionDto> GetByIdAsync(string id)
        {
            var question = await _context.Questions.FindAsync(id);
            return question.AdaptToDto();
        }

        public async Task UpdateEntity(QuestionDto model)
        {
            _helper.ThrowValidationExceptionIfModelIsNull(await _context.Questions.FindAsync(model.Id));
            await _validator.ValidateAsync(model);

            _context.Questions.Update(model.AdaptToQuestion());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(string id)
        {
            var question = await _context.Questions.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(question);

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
        }
    }
}