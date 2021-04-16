using System.Collections.Generic;

namespace QuizApp.Business.Models
{
    public partial class QuestionDto
    {
        public int QuestionNumber { get; set; }
        public string TopicId { get; set; }
        public string Id { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }
    }
}