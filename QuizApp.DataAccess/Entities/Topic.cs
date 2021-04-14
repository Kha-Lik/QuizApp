using System.Collections.Generic;

namespace QuizApp.DataAccess.Entities
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; }
        public int TopicNumber { get; set; }
        public int TimeToPass { get; set; }
        public int QuestionsPerAttempt { get; set; }
        public int MaxAttemptCount { get; set; }
        
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}