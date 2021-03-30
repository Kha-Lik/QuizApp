namespace Quiz_DAL.Entities
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; }
        public int TopicNumber { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}