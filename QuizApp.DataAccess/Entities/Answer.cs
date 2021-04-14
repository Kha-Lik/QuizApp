namespace QuizApp.DataAccess.Entities
{
    public class Answer : BaseEntity
    {
        public string QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }

        public Question Question { get; set; }
    }
}