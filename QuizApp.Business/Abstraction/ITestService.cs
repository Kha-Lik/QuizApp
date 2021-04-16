using System.Collections.Generic;
using System.Threading.Tasks;
using QuizApp.Business.Models;

namespace QuizApp.Business.Abstraction
{
    public interface ITestService
    {
        Task<IEnumerable<AttemptDto>> GetTestResultsForStudentAsync(UserDto student, string topicId);
        Task<TestModel> GenerateTestForTopicAsync(UserDto student, string topicId);
        Task SubmitTestAsync(TestModel test);
    }
}