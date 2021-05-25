using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using QuizApp.Business.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using QuizApp.IntegrationTests.Factory;
using System.Text;

namespace QuizApp.IntegrationTests.Tests
{
    [TestFixture]
    public class AnswersIntegrationTests
    {
        private HttpClient _client;
        private CustomAppFactory _factory;
        private const string RequestUri = "api/answer/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task AnswerController_GetAll_ReturnsAnswers()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<AnswerDto>>(stringResponse);

            answers.Count().Should().Be(4);
        }

        [Test]
        public async Task AnswerController_GetById_ReturnsAnswerDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + "answer1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<AnswerDto>(stringResponse);

            answer.Id.Should().Be("answer1");
            answer.AnswerText.Should().Be("Here");
        }

        [Test]
        public async Task AnswerController_GetByQuestionId_ReturnsAnswerDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + "question=" + "question1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<IEnumerable<AnswerDto>>(stringResponse);

            answer.Count().Should().Be(2);
        }

        [Test]
        public async Task AnswerController_Update_UpdatesAnswer()
        {
            var updatedAnswer = new AnswerDto { Id = "answer2", AnswerText = "Hitler is a bag guy", IsCorrect = true, QuestionId = "question1" };
            var payload = JsonConvert.SerializeObject(updatedAnswer);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri, content);

            var actualHttpResponse = await _client.GetAsync(RequestUri + "answer2");

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actualHttpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<AnswerDto>(stringActualResponse);
            answer.Id.Should().Be("answer2");
            answer.AnswerText.Should().Be("Hitler is a bag guy");
        }
        
        [Test]
        public async Task AnswerController_Create_CreatesAnswer()
        {
            var updatedAnswer = new AnswerDto { Id = "answer5", AnswerText = "Wrong answer", IsCorrect = false, QuestionId = "question2" };
            var payload = JsonConvert.SerializeObject(updatedAnswer);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            var actual = await _client.GetAsync(RequestUri + "answer5");

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actual.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<AnswerDto>(stringActualResponse);
            answer.Id.Should().Be("answer5");
            answer.AnswerText.Should().Be("Wrong answer");
        }

        [Test]
        public async Task AnswerController_Delete_DeletesAnswer()
        {
            var actualRequestBeforeDelete = await _client.GetAsync(RequestUri);
            actualRequestBeforeDelete.EnsureSuccessStatusCode();
            var stringBeforeDeleteResponse = await actualRequestBeforeDelete.Content.ReadAsStringAsync();
            var categoriesBeforeDelete = JsonConvert.DeserializeObject<IEnumerable<AnswerDto>>(stringBeforeDeleteResponse);


            var httpResponse = await _client.DeleteAsync(RequestUri + "answer3");

            var actualRequestAfterDelete = await _client.GetAsync(RequestUri);
            actualRequestAfterDelete.EnsureSuccessStatusCode();
            var stringAfterDeleteResponse = await actualRequestAfterDelete.Content.ReadAsStringAsync();
            var categoriesAfterDelete = JsonConvert.DeserializeObject<IEnumerable<AnswerDto>>(stringAfterDeleteResponse);

            httpResponse.EnsureSuccessStatusCode();
            categoriesAfterDelete.Count().Should().Be(categoriesBeforeDelete.Count() - 1);
            ;
        }
    }
}