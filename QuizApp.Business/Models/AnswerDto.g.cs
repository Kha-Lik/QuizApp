namespace QuizApp.Business.Models
{
    public partial class AnswerDto
    {
        public string QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public string Id { get; set; }
    }
}