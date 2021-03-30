namespace Quiz_DAL.Entities
{
    public class QuestionResult : BaseEntity
    {
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public bool Result { get; set; }
        public Attempt Attempt { get; set; }
        public Question Question { get; set; }
    }
}