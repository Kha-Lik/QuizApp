using System.Collections.Generic;
using QuizApp.Business.Models;

namespace QuizApp.Business.Models
{
    public partial class TopicDto
    {
        public string Name { get; set; }
        public int TopicNumber { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
        public int SubjectId { get; set; }
        public int Id { get; set; }
    }
}