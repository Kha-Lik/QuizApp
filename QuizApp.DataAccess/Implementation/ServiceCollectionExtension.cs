using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.DataAccess.Entities;

namespace QuizApp.DataAccess.Implementation
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddDalServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<QuizDbContext>(builder => builder.UseSqlServer(connectionString));
            //services.AddDbContext<QuizDbContext>(builder => builder.UseInMemoryDatabase("QuizDB"));
            services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = true;
                opt.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<QuizDbContext>()
            .AddSignInManager();
            return services;
        }
    }
}