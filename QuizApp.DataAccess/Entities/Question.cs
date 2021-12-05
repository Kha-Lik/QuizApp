namespace QuizApp.DataAccess.Entities
{
    public class Question : BaseEntity
    {
        public int QuestionNumber { get; set; }
        public string QuestionText { get; set; }
        public string TopicId { get; set; }
        
        public Topic Topic { get; set; }
    }
}