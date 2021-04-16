using System;
using System.Linq.Expressions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Entities;

namespace QuizApp.Business.Extensions
{
    public static partial class AnswerMapper
    {
        public static Answer AdaptToAnswer(this AnswerDto p1)
        {
            return p1 == null ? null : new Answer()
            {
                QuestionId = p1.QuestionId,
                AnswerText = p1.AnswerText,
                IsCorrect = p1.IsCorrect,
                Id = p1.Id
            };
        }
        public static Answer AdaptTo(this AnswerDto p2, Answer p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Answer result = p3 ?? new Answer();
            
            result.QuestionId = p2.QuestionId;
            result.AnswerText = p2.AnswerText;
            result.IsCorrect = p2.IsCorrect;
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<AnswerDto, Answer>> ProjectToAnswer => p4 => new Answer()
        {
            QuestionId = p4.QuestionId,
            AnswerText = p4.AnswerText,
            IsCorrect = p4.IsCorrect,
            Id = p4.Id
        };
        public static AnswerDto AdaptToDto(this Answer p5)
        {
            return p5 == null ? null : new AnswerDto()
            {
                QuestionId = p5.QuestionId,
                AnswerText = p5.AnswerText,
                IsCorrect = p5.IsCorrect,
                Id = p5.Id
            };
        }
        public static AnswerDto AdaptTo(this Answer p6, AnswerDto p7)
        {
            if (p6 == null)
            {
                return null;
            }
            AnswerDto result = p7 ?? new AnswerDto();
            
            result.QuestionId = p6.QuestionId;
            result.AnswerText = p6.AnswerText;
            result.IsCorrect = p6.IsCorrect;
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<Answer, AnswerDto>> ProjectToDto => p8 => new AnswerDto()
        {
            QuestionId = p8.QuestionId,
            AnswerText = p8.AnswerText,
            IsCorrect = p8.IsCorrect,
            Id = p8.Id
        };
    }
}