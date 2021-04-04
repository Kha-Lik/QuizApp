using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace QuizApp.DataAccess.Entities
{
    public class User : IdentityUser
    {
        [DataMember]
        public override string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
    }
}