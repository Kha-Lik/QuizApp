using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using QuizApp.Business.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using QuizApp.IntegrationTests.Factory;

namespace QuizApp.IntegrationTests.Tests
{
    [TestFixture]
    public class AnswersIntegrationTests
    {
        private HttpClient _client;
        private CustomAppFactory _factory;
        private const string RequestUrl = "api/answer/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomAppFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task AnswerController_GetAll_ReturnsAnswers()
        {
            var httpResponse = await _client.GetAsync(RequestUrl);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answers = JsonConvert.DeserializeObject<IEnumerable<AnswerDto>>(stringResponse);

            answers.Count().Should().Be(4);
        }

        [Test]
        public async Task AnswerController_GetById_ReturnsAnswerDto()
        {
            var httpResponse = await _client.GetAsync(RequestUrl + "answer1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<AnswerDto>(stringResponse);

            answer.Id.Should().Be("answer1");
            answer.AnswerText.Should().Be("Here");
        }

        [Test]
        public async Task AnswerController_GetByQuestionId_ReturnsAnswerDto()
        {
            var httpResponse = await _client.GetAsync(RequestUrl + "question=" + "question1");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var answer = JsonConvert.DeserializeObject<IEnumerable<AnswerDto>>(stringResponse);

            answer.Count().Should().Be(2);
        }
    }
}