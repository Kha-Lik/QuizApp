using System;
using System.Linq;
using System.Linq.Expressions;
using QuizApp.DataAccess.Entities;
using QuizApp.Business.Models;

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
                QuestionResults = p1.QuestionResults == null ? null : p1.QuestionResults.Select<QuestionResultDto, QuestionResult>(funcMain1),
                Id = p1.Id
            };
        }
        public static Attempt AdaptTo(this AttemptDto p3, Attempt p4)
        {
            if (p3 == null)
            {
                return null;
            }
            Attempt result = p4 ?? new Attempt();
            
            result.StudentId = p3.StudentId;
            result.TopicId = p3.TopicId;
            result.Score = p3.Score;
            result.DateTime = p3.DateTime;
            result.QuestionResults = p3.QuestionResults == null ? null : p3.QuestionResults.Select<QuestionResultDto, QuestionResult>(funcMain2);
            result.Id = p3.Id;
            return result;
            
        }
        public static Expression<Func<AttemptDto, Attempt>> ProjectToAttempt => p6 => new Attempt()
        {
            StudentId = p6.StudentId,
            TopicId = p6.TopicId,
            Score = p6.Score,
            DateTime = p6.DateTime,
            QuestionResults = p6.QuestionResults.Select<QuestionResultDto, QuestionResult>(p7 => new QuestionResult()
            {
                AttemptId = p7.AttemptId,
                QuestionId = p7.QuestionId,
                Result = p7.Result,
                Id = p7.Id
            }),
            Id = p6.Id
        };
        public static AttemptDto AdaptToDto(this Attempt p8)
        {
            return p8 == null ? null : new AttemptDto()
            {
                StudentId = p8.StudentId,
                TopicId = p8.TopicId,
                Score = p8.Score,
                DateTime = p8.DateTime,
                QuestionResults = p8.QuestionResults == null ? null : p8.QuestionResults.Select<QuestionResult, QuestionResultDto>(funcMain3),
                Id = p8.Id
            };
        }
        public static AttemptDto AdaptTo(this Attempt p10, AttemptDto p11)
        {
            if (p10 == null)
            {
                return null;
            }
            AttemptDto result = p11 ?? new AttemptDto();
            
            result.StudentId = p10.StudentId;
            result.TopicId = p10.TopicId;
            result.Score = p10.Score;
            result.DateTime = p10.DateTime;
            result.QuestionResults = p10.QuestionResults == null ? null : p10.QuestionResults.Select<QuestionResult, QuestionResultDto>(funcMain4);
            result.Id = p10.Id;
            return result;
            
        }
        public static Expression<Func<Attempt, AttemptDto>> ProjectToDto => p13 => new AttemptDto()
        {
            StudentId = p13.StudentId,
            TopicId = p13.TopicId,
            Score = p13.Score,
            DateTime = p13.DateTime,
            QuestionResults = p13.QuestionResults.Select<QuestionResult, QuestionResultDto>(p14 => new QuestionResultDto()
            {
                AttemptId = p14.AttemptId,
                QuestionId = p14.QuestionId,
                Result = p14.Result,
                Id = p14.Id
            }),
            Id = p13.Id
        };
        
        private static QuestionResult funcMain1(QuestionResultDto p2)
        {
            return p2 == null ? null : new QuestionResult()
            {
                AttemptId = p2.AttemptId,
                QuestionId = p2.QuestionId,
                Result = p2.Result,
                Id = p2.Id
            };
        }
        
        private static QuestionResult funcMain2(QuestionResultDto p5)
        {
            return p5 == null ? null : new QuestionResult()
            {
                AttemptId = p5.AttemptId,
                QuestionId = p5.QuestionId,
                Result = p5.Result,
                Id = p5.Id
            };
        }
        
        private static QuestionResultDto funcMain3(QuestionResult p9)
        {
            return p9 == null ? null : new QuestionResultDto()
            {
                AttemptId = p9.AttemptId,
                QuestionId = p9.QuestionId,
                Result = p9.Result,
                Id = p9.Id
            };
        }
        
        private static QuestionResultDto funcMain4(QuestionResult p12)
        {
            return p12 == null ? null : new QuestionResultDto()
            {
                AttemptId = p12.AttemptId,
                QuestionId = p12.QuestionId,
                Result = p12.Result,
                Id = p12.Id
            };
        }
    }
}