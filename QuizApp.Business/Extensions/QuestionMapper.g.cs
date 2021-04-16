using System;
using System.Linq.Expressions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Entities;

namespace QuizApp.Business.Extensions
{
    public static partial class QuestionMapper
    {
        public static Question AdaptToQuestion(this QuestionDto p1)
        {
            return p1 == null ? null : new Question()
            {
                QuestionNumber = p1.QuestionNumber,
                TopicId = p1.TopicId,
                Id = p1.Id
            };
        }
        public static Question AdaptTo(this QuestionDto p2, Question p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Question result = p3 ?? new Question();
            
            result.QuestionNumber = p2.QuestionNumber;
            result.TopicId = p2.TopicId;
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<QuestionDto, Question>> ProjectToQuestion => p4 => new Question()
        {
            QuestionNumber = p4.QuestionNumber,
            TopicId = p4.TopicId,
            Id = p4.Id
        };
        public static QuestionDto AdaptToDto(this Question p5)
        {
            return p5 == null ? null : new QuestionDto()
            {
                QuestionNumber = p5.QuestionNumber,
                TopicId = p5.TopicId,
                Id = p5.Id
            };
        }
        public static QuestionDto AdaptTo(this Question p6, QuestionDto p7)
        {
            if (p6 == null)
            {
                return null;
            }
            QuestionDto result = p7 ?? new QuestionDto();
            
            result.QuestionNumber = p6.QuestionNumber;
            result.TopicId = p6.TopicId;
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<Question, QuestionDto>> ProjectToDto => p8 => new QuestionDto()
        {
            QuestionNumber = p8.QuestionNumber,
            TopicId = p8.TopicId,
            Id = p8.Id
        };
    }
}