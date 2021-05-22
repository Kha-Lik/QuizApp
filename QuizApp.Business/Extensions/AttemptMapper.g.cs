using System;
using System.Linq.Expressions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Entities;

namespace QuizApp.Business.Extensions
{
    public static partial class AttemptMapper
    {
        public static Attempt AdaptToAttempt(this AttemptDto p1)
        {
            return p1 == null ? null : new Attempt()
            {
                StudentId = p1.StudentId,
                TopicId = p1.TopicId,
                Score = p1.Score,
                DateTime = p1.DateTime,
                Id = p1.Id
            };
        }
        public static Attempt AdaptTo(this AttemptDto p2, Attempt p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Attempt result = p3 ?? new Attempt();
            
            result.StudentId = p2.StudentId;
            result.TopicId = p2.TopicId;
            result.Score = p2.Score;
            result.DateTime = p2.DateTime;
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<AttemptDto, Attempt>> ProjectToAttempt => p4 => new Attempt()
        {
            StudentId = p4.StudentId,
            TopicId = p4.TopicId,
            Score = p4.Score,
            DateTime = p4.DateTime,
            Id = p4.Id
        };
        public static AttemptDto AdaptToDto(this Attempt p5)
        {
            return p5 == null ? null : new AttemptDto()
            {
                StudentId = p5.StudentId,
                TopicId = p5.TopicId,
                Score = p5.Score,
                DateTime = p5.DateTime,
                Id = p5.Id
            };
        }
        public static AttemptDto AdaptTo(this Attempt p6, AttemptDto p7)
        {
            if (p6 == null)
            {
                return null;
            }
            AttemptDto result = p7 ?? new AttemptDto();
            
            result.StudentId = p6.StudentId;
            result.TopicId = p6.TopicId;
            result.Score = p6.Score;
            result.DateTime = p6.DateTime;
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<Attempt, AttemptDto>> ProjectToDto => p8 => new AttemptDto()
        {
            StudentId = p8.StudentId,
            TopicId = p8.TopicId,
            Score = p8.Score,
            DateTime = p8.DateTime,
            Id = p8.Id
        };
    }
}