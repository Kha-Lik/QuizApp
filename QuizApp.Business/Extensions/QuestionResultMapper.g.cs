using System;
using System.Linq.Expressions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Entities;

namespace QuizApp.Business.Extensions
{
    public static partial class QuestionResultMapper
    {
        public static QuestionResult AdaptToQuestionResult(this QuestionResultDto p1)
        {
            return p1 == null ? null : new QuestionResult()
            {
                AttemptId = p1.AttemptId,
                QuestionId = p1.QuestionId,
                Result = p1.Result,
                Id = p1.Id
            };
        }
        public static QuestionResult AdaptTo(this QuestionResultDto p2, QuestionResult p3)
        {
            if (p2 == null)
            {
                return null;
            }
            QuestionResult result = p3 ?? new QuestionResult();
            
            result.AttemptId = p2.AttemptId;
            result.QuestionId = p2.QuestionId;
            result.Result = p2.Result;
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<QuestionResultDto, QuestionResult>> ProjectToQuestionResult => p4 => new QuestionResult()
        {
            AttemptId = p4.AttemptId,
            QuestionId = p4.QuestionId,
            Result = p4.Result,
            Id = p4.Id
        };
        public static QuestionResultDto AdaptToDto(this QuestionResult p5)
        {
            return p5 == null ? null : new QuestionResultDto()
            {
                AttemptId = p5.AttemptId,
                QuestionId = p5.QuestionId,
                Result = p5.Result,
                Id = p5.Id
            };
        }
        public static QuestionResultDto AdaptTo(this QuestionResult p6, QuestionResultDto p7)
        {
            if (p6 == null)
            {
                return null;
            }
            QuestionResultDto result = p7 ?? new QuestionResultDto();
            
            result.AttemptId = p6.AttemptId;
            result.QuestionId = p6.QuestionId;
            result.Result = p6.Result;
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<QuestionResult, QuestionResultDto>> ProjectToDto => p8 => new QuestionResultDto()
        {
            AttemptId = p8.AttemptId,
            QuestionId = p8.QuestionId,
            Result = p8.Result,
            Id = p8.Id
        };
    }
}