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
    public class TopicService : ICrudInterface<TopicDto>
    {
        private readonly QuizDbContext _context;
        private readonly IServiceHelper<Topic> _helper;
        private readonly AbstractValidator<TopicDto> _validator;

        public TopicService(QuizDbContext context, IServiceHelper<Topic> helper, AbstractValidator<TopicDto> validator)
        {
            _context = context;
            _helper = helper;
            _validator = validator;
        }

        public async Task CreateEntityAsync(TopicDto model)
        {
            await _validator.ValidateAsync(model);
            await _context.Topics.AddAsync(model.AdaptToTopic());

            await _context.SaveChangesAsync();
        }

        public IAsyncEnumerable<TopicDto> GetEntitiesByPrincipalId(string principalId)
        {
            return _context.Topics.Where(t => t.SubjectId.Equals(principalId)).Select(TopicMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public IAsyncEnumerable<TopicDto> GetAllAsync()
        {
            return _context.Topics.Select(TopicMapper.ProjectToDto).AsAsyncEnumerable();
        }

        public async Task<TopicDto> GetByIdAsync(string id)
        {
            var topic = await _context.Topics.FindAsync(id);
            return topic.AdaptToDto();
        }

        public async Task UpdateEntity(TopicDto model)
        {
            _helper.ThrowValidationExceptionIfModelIsNull(await _context.Topics.FindAsync(model.Id));
            await _validator.ValidateAsync(model);

            _context.Topics.Update(model.AdaptToTopic());
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEntityByIdAsync(string id)
        {
            var topic = await _context.Topics.FindAsync(id);
            _helper.ThrowValidationExceptionIfModelIsNull(topic);

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();
        }
    }
}