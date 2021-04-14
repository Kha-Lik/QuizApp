namespace QuizApp.Business.Models
{
    public partial class QuestionResultDto
    {
        public string AttemptId { get; set; }
        public string QuestionId { get; set; }
        public bool Result { get; set; }
        public string Id { get; set; }
    }
}