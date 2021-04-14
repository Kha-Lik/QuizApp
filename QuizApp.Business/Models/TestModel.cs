using System;
using System.Collections.Generic;

namespace QuizApp.Business.Models
{
    public class TestModel
    {
        public UserDto Student { get; set; }
        public TopicDto Topic { get; set; }
        public DateTime DateTimePassed { get; set; }
        public int Score { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
    }
}