using System.Collections.Generic;
using Quiz_BLL.Models;

namespace Quiz_BLL.Models
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