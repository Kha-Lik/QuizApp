using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Business.Abstraction
{
    public interface ICrudInterface<TDto>
    {
        Task CreateEntityAsync(TDto model);

        IAsyncEnumerable<TDto> GetEntitiesByPrincipalId(string principalId);

        IAsyncEnumerable<TDto> GetAllAsync();

        Task<TDto> GetByIdAsync(string id);

        Task UpdateEntity(TDto model);

        Task DeleteEntityByIdAsync(string id);
    }
}