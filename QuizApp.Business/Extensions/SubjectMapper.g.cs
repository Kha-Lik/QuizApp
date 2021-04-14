using System;
using System.Linq.Expressions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Entities;

namespace QuizApp.Business.Extensions
{
    public static partial class SubjectMapper
    {
        public static Subject AdaptToSubject(this SubjectDto p1)
        {
            return p1 == null ? null : new Subject()
            {
                Name = p1.Name,
                LecturerId = p1.LecturerId,
                Id = p1.Id
            };
        }
        public static Subject AdaptTo(this SubjectDto p2, Subject p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Subject result = p3 ?? new Subject();
            
            result.Name = p2.Name;
            result.LecturerId = p2.LecturerId;
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<SubjectDto, Subject>> ProjectToSubject => p4 => new Subject()
        {
            Name = p4.Name,
            LecturerId = p4.LecturerId,
            Id = p4.Id
        };
        public static SubjectDto AdaptToDto(this Subject p5)
        {
            return p5 == null ? null : new SubjectDto()
            {
                Name = p5.Name,
                LecturerId = p5.LecturerId,
                Id = p5.Id
            };
        }
        public static SubjectDto AdaptTo(this Subject p6, SubjectDto p7)
        {
            if (p6 == null)
            {
                return null;
            }
            SubjectDto result = p7 ?? new SubjectDto();
            
            result.Name = p6.Name;
            result.LecturerId = p6.LecturerId;
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<Subject, SubjectDto>> ProjectToDto => p8 => new SubjectDto()
        {
            Name = p8.Name,
            LecturerId = p8.LecturerId,
            Id = p8.Id
        };
    }
}