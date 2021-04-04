using System.Collections.Generic;

namespace QuizApp.DataAccess.Entities
{
    public class Subject : BaseEntity
    {
        public string Name { get; set; }
        public string LecturerId { get; set; }
        public User Lecturer { get; set; }
        public IEnumerable<Topic> Topics { get; set; }
    }
}