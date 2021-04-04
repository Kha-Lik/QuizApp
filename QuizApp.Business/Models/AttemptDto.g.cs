using System;
using System.Collections.Generic;
using QuizApp.Business.Models;

namespace QuizApp.Business.Models
{
    public partial class AttemptDto
    {
        public string StudentId { get; set; }
        public int TopicId { get; set; }
        public int Score { get; set; }
        public DateTime DateTime { get; set; }
        public IEnumerable<QuestionResultDto> QuestionResults { get; set; }
        public int Id { get; set; }
    }
}