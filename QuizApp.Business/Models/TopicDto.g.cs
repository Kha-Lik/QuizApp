namespace QuizApp.Business.Models
{
    public partial class TopicDto
    {
        public string Name { get; set; }
        public int TopicNumber { get; set; }
        public int TimeToPass { get; set; }
        public int QuestionsPerAttempt { get; set; }
        public int MaxAttemptCount { get; set; }
        public string SubjectId { get; set; }
        public string Id { get; set; }
    }
}