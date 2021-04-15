using System.ComponentModel.DataAnnotations;
using QuizApp.Business.Abstraction;
using QuizApp.DataAccess.Entities;

namespace QuizApp.Business.Implementation.Validation
{
    public class ServiceHelper<TEntity> : IServiceHelper<TEntity> where TEntity : BaseEntity
    {
        public void ThrowValidationExceptionIfModelIsNull(TEntity entity)
        {
            if (entity is null)
            {
                throw new ValidationException($"{nameof(entity)} is null.");
            }
        }
    }
}