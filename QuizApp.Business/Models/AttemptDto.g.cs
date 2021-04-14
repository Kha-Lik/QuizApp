using System;

namespace QuizApp.Business.Models
{
    public partial class AttemptDto
    {
        public string StudentId { get; set; }
        public string TopicId { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
        public string Id { get; set; }
    }
}