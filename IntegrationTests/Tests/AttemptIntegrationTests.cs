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
    public class AttemptsIntegrationTests
    {
        private HttpClient _client;
        private CustomAppFactory _factory;
        private const string RequestUri = "api/attempt/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task AttemptController_GetAll_ReturnsAttempts()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var attempts = JsonConvert.DeserializeObject<IEnumerable<AttemptDto>>(stringResponse);

            attempts.Count().Should().Be(2);
        }

        [Test]
        public async Task AttemptController_GetById_ReturnsAttemptDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + "attempt1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var attempt = JsonConvert.DeserializeObject<AttemptDto>(stringResponse);

            attempt.Id.Should().Be("attempt1");
            attempt.Score.Should().Be(100);
        }

        [Test]
        public async Task AttemptController_GetByStudentId_ReturnsAttemptDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + "student=" + "1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var attempt = JsonConvert.DeserializeObject<IEnumerable<AttemptDto>>(stringResponse);

            attempt.Count().Should().Be(2);
        }

        [Test]
        public async Task AttemptController_Update_UpdatesAttempt()
        {
            var updatedAttempt = new AttemptDto { Id = "attempt1", DateTime = DateTime.Now, Score = 58, StudentId = "1", TopicId = "topic2" };
            var payload = JsonConvert.SerializeObject(updatedAttempt);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri, content);

            var actualHttpResponse = await _client.GetAsync(RequestUri + "attempt1");

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actualHttpResponse.Content.ReadAsStringAsync();
            var attempt = JsonConvert.DeserializeObject<AttemptDto>(stringActualResponse);
            attempt.Id.Should().Be("attempt1");
            attempt.Score.Should().Be(58);
        }

        [Test]
        public async Task AttemptController_Create_CreatesAttempt()
        {
            var updatedAttempt = new AttemptDto { Id = "attempt2", DateTime = DateTime.Now, Score = 32, StudentId = "1", TopicId = "topic1" };
            var payload = JsonConvert.SerializeObject(updatedAttempt);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            var actual = await _client.GetAsync(RequestUri + "attempt2");

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actual.Content.ReadAsStringAsync();
            var attempt = JsonConvert.DeserializeObject<AttemptDto>(stringActualResponse);
            attempt.Id.Should().Be("attempt2");
            attempt.Score.Should().Be(32);
        }

        [Test]
        public async Task AttemptController_Delete_DeletesAttempt()
        {
            var actualRequestBeforeDelete = await _client.GetAsync(RequestUri);
            actualRequestBeforeDelete.EnsureSuccessStatusCode();
            var stringBeforeDeleteResponse = await actualRequestBeforeDelete.Content.ReadAsStringAsync();
            var categoriesBeforeDelete = JsonConvert.DeserializeObject<IEnumerable<AttemptDto>>(stringBeforeDeleteResponse);


            var httpResponse = await _client.DeleteAsync(RequestUri + "attempt1");

            var actualRequestAfterDelete = await _client.GetAsync(RequestUri);
            actualRequestAfterDelete.EnsureSuccessStatusCode();
            var stringAfterDeleteResponse = await actualRequestAfterDelete.Content.ReadAsStringAsync();
            var categoriesAfterDelete = JsonConvert.DeserializeObject<IEnumerable<AttemptDto>>(stringAfterDeleteResponse);

            httpResponse.EnsureSuccessStatusCode();
            categoriesAfterDelete.Count().Should().Be(categoriesBeforeDelete.Count() - 1);
            ;
        }
    }
}