namespace Quiz_BLL.Models
{
    public partial class QuestionResultDto
    {
        public int AttemptId { get; set; }
        public int QuestionId { get; set; }
        public bool Result { get; set; }
        public int Id { get; set; }
    }
}