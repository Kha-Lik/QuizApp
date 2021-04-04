namespace QuizApp.DataAccess.Entities
{
    public class Question : BaseEntity
    {
        public int QuestionNumber { get; set; }
        public int TopicId { get; set; }
        public Topic Topic { get; set; }
    }
}