using QuizApp.DataAccess.Entities;

namespace QuizApp.Business.Abstraction
{
    public interface IServiceHelper<in TEntity> where TEntity : BaseEntity
    {
        void ThrowValidationExceptionIfModelIsNull(TEntity entity);
    }
}