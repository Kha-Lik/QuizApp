using System.Collections.Generic;

namespace QuizApp.DataAccess.Entities
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; }
        public int TopicNumber { get; set; }
        public IEnumerable<Question> Questions { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}