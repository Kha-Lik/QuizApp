using System.Collections.Generic;
using Quiz_BLL.Models;

namespace Quiz_BLL.Models
{
    public partial class SubjectDto
    {
        public string Name { get; set; }
        public string LecturerId { get; set; }
        public IEnumerable<TopicDto> Topics { get; set; }
        public int Id { get; set; }
    }
}