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
    public class QuestionsIntegrationTests
    {
        private HttpClient _client;
        private CustomAppFactory _factory;
        private const string RequestUri = "api/question/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task QuestionController_GetAll_ReturnsQuestions()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var questions = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(stringResponse);

            questions.Count().Should().Be(2);
        }

        [Test]
        public async Task QuestionController_GetById_ReturnsQuestionDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + "question1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<QuestionDto>(stringResponse);

            question.Id.Should().Be("question1");
            question.QuestionText.Should().Be("Where?");
        }

        [Test]
        public async Task QuestionController_GetByTopicId_ReturnsQuestionDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + "topic=" + "topic2");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(stringResponse);

            question.Count().Should().Be(1);
        }

        [Test]
        public async Task QuestionController_Update_UpdatesQuestion()
        {
            var updatedQuestion = new QuestionDto { Id = "question1", QuestionNumber = 1, TopicId = "topic1", QuestionText = "Aboba?" };
            var payload = JsonConvert.SerializeObject(updatedQuestion);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri, content);

            var actualHttpResponse = await _client.GetAsync(RequestUri + "question1");

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actualHttpResponse.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<QuestionDto>(stringActualResponse);
            question.Id.Should().Be("question1");
            question.QuestionText.Should().Be("Aboba?");
        }

        [Test]
        public async Task QuestionController_Create_CreatesQuestion()
        {
            var updatedQuestion = new QuestionDto { Id = "question3", QuestionNumber = 3, TopicId = "topic1", QuestionText = "Why not?" };
            var payload = JsonConvert.SerializeObject(updatedQuestion);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            var actual = await _client.GetAsync(RequestUri + "question3");

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actual.Content.ReadAsStringAsync();
            var question = JsonConvert.DeserializeObject<QuestionDto>(stringActualResponse);
            question.Id.Should().Be("question3");
            question.QuestionText.Should().Be("Why not?");
        }

        [Test]
        public async Task QuestionController_Delete_DeletesQuestion()
        {
            var actualRequestBeforeDelete = await _client.GetAsync(RequestUri);
            actualRequestBeforeDelete.EnsureSuccessStatusCode();
            var stringBeforeDeleteResponse = await actualRequestBeforeDelete.Content.ReadAsStringAsync();
            var categoriesBeforeDelete = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(stringBeforeDeleteResponse);


            var httpResponse = await _client.DeleteAsync(RequestUri + "question1");

            var actualRequestAfterDelete = await _client.GetAsync(RequestUri);
            actualRequestAfterDelete.EnsureSuccessStatusCode();
            var stringAfterDeleteResponse = await actualRequestAfterDelete.Content.ReadAsStringAsync();
            var categoriesAfterDelete = JsonConvert.DeserializeObject<IEnumerable<QuestionDto>>(stringAfterDeleteResponse);

            httpResponse.EnsureSuccessStatusCode();
            categoriesAfterDelete.Count().Should().Be(categoriesBeforeDelete.Count() - 1);
            ;
        }
    }
}