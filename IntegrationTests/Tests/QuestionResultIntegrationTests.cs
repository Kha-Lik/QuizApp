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
using System;

namespace QuizApp.IntegrationTests.Tests
{
    [TestFixture]
    public class QuestionResultsIntegrationTests
    {
        private HttpClient _client;
        private CustomAppFactory _factory;
        private const string RequestUri = "api/questionResult/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task QuestionResultController_GetAll_ReturnsQuestionResults()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var questionResults = JsonConvert.DeserializeObject<IEnumerable<QuestionResultDto>>(stringResponse);

            questionResults.Count().Should().Be(2);
        }

        [Test]
        public async Task QuestionResultController_GetById_ReturnsQuestionResultDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + "questionResult1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var questionResult = JsonConvert.DeserializeObject<QuestionResultDto>(stringResponse);

            questionResult.Id.Should().Be("questionResult1");
            questionResult.Result.Should().Be(true);
        }

        [Test]
        public async Task QuestionResultController_GetByAttemptId_ReturnsQuestionResultDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + "attempt=" + "attempt1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var questionResult = JsonConvert.DeserializeObject<IEnumerable<QuestionResultDto>>(stringResponse);

            questionResult.Count().Should().Be(1);
        }

        [Test]
        public async Task QuestionResultController_Update_UpdatesQuestionResult()
        {
            var updatedQuestionResult = new QuestionResultDto { Id = "questionResult1", AttemptId = "attempt1", QuestionId = "question2", Result = false };
            var payload = JsonConvert.SerializeObject(updatedQuestionResult);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri, content);

            var actualHttpResponse = await _client.GetAsync(RequestUri + "questionResult1");

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actualHttpResponse.Content.ReadAsStringAsync();
            var questionResult = JsonConvert.DeserializeObject<QuestionResultDto>(stringActualResponse);
            questionResult.Id.Should().Be("questionResult1");
            questionResult.Result.Should().Be(false);
        }

        [Test]
        public async Task QuestionResultController_Create_CreatesQuestionResult()
        {
            var updatedQuestionResult = new QuestionResultDto { Id = "questionResult2", AttemptId = "attempt1", QuestionId = "question2", Result = true };
            var payload = JsonConvert.SerializeObject(updatedQuestionResult);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            var actual = await _client.GetAsync(RequestUri + "questionResult2");

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actual.Content.ReadAsStringAsync();
            var questionResult = JsonConvert.DeserializeObject<QuestionResultDto>(stringActualResponse);
            questionResult.Id.Should().Be("questionResult2");
            questionResult.Result.Should().Be(true);
        }

        [Test]
        public async Task QuestionResultController_Delete_DeletesQuestionResult()
        {
            var actualRequestBeforeDelete = await _client.GetAsync(RequestUri);
            actualRequestBeforeDelete.EnsureSuccessStatusCode();
            var stringBeforeDeleteResponse = await actualRequestBeforeDelete.Content.ReadAsStringAsync();
            var categoriesBeforeDelete = JsonConvert.DeserializeObject<IEnumerable<QuestionResultDto>>(stringBeforeDeleteResponse);


            var httpResponse = await _client.DeleteAsync(RequestUri + "questionResult1");

            var actualRequestAfterDelete = await _client.GetAsync(RequestUri);
            actualRequestAfterDelete.EnsureSuccessStatusCode();
            var stringAfterDeleteResponse = await actualRequestAfterDelete.Content.ReadAsStringAsync();
            var categoriesAfterDelete = JsonConvert.DeserializeObject<IEnumerable<QuestionResultDto>>(stringAfterDeleteResponse);

            httpResponse.EnsureSuccessStatusCode();
            categoriesAfterDelete.Count().Should().Be(categoriesBeforeDelete.Count() - 1);
            ;
        }
    }
}