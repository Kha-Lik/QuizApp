using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz_DAL.Entities;

namespace Quiz_DAL.Implementations
{
    public class QuizDbContext : IdentityDbContext<User>
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        {
            base.Database.EnsureCreated();
        }
        
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<QuestionResult> QuestionResults { get; set; }
    }
}