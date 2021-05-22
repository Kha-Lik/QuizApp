namespace QuizApp.DataAccess.Entities
{
    public class QuestionResult : BaseEntity
    {
        public string AttemptId { get; set; }
        public string QuestionId { get; set; }
        public bool Result { get; set; }
        
        public Attempt Attempt { get; set; }
        public Question Question { get; set; }
    }
}