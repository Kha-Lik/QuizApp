using System;
using System.Linq.Expressions;
using QuizApp.DataAccess.Entities;
using QuizApp.Business.Models;

namespace QuizApp.Business.Extensions
{
    public static partial class UserMapper
    {
        public static User AdaptToUser(this UserDto p1)
        {
            return p1 == null ? null : new User()
            {
                Id = p1.Id,
                Name = p1.Name,
                Surname = p1.Surname
            };
        }
        public static User AdaptTo(this UserDto p2, User p3)
        {
            if (p2 == null)
            {
                return null;
            }
            User result = p3 ?? new User();

            result.Id = p2.Id;
            result.Name = p2.Name;
            result.Surname = p2.Surname;
            return result;
            
        }
        public static Expression<Func<UserDto, User>> ProjectToUser => p4 => new User()
        {
            Id = p4.Id,
            Name = p4.Name,
            Surname = p4.Surname
        };
        public static UserDto AdaptToDto(this User p5)
        {
            return p5 == null ? null : new UserDto()
            {
                Id = p5.Id,
                Name = p5.Name,
                Surname = p5.Surname
            };
        }
        public static UserDto AdaptTo(this User p6, UserDto p7)
        {
            if (p6 == null)
            {
                return null;
            }
            UserDto result = p7 ?? new UserDto();
            
            result.Id = p6.Id;
            result.Name = p6.Name;
            result.Surname = p6.Surname;
            return result;
            
        }
        public static Expression<Func<User, UserDto>> ProjectToDto => p8 => new UserDto()
        {
            Id = p8.Id,
            Name = p8.Name,
            Surname = p8.Surname
        };

        public static User AdaptToUser(this UserRegistrationModel p9)
        {
            return p9 == null ? null : new User()
            {
                Name = p9.Name,
                Surname = p9.Surname,
                UserName = p9.Email,
                Email = p9.Email
            };
        }
    }
}