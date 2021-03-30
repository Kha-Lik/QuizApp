using Microsoft.AspNetCore.Identity;

namespace Quiz_DAL.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}