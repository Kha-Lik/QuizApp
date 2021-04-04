using System;
using System.Linq;
using System.Linq.Expressions;
using QuizApp.DataAccess.Entities;
using QuizApp.Business.Models;

namespace QuizApp.Business.Extensions
{
    public static partial class TopicMapper
    {
        public static Topic AdaptToTopic(this TopicDto p1)
        {
            return p1 == null ? null : new Topic()
            {
                Name = p1.Name,
                TopicNumber = p1.TopicNumber,
                Questions = p1.Questions == null ? null : p1.Questions.Select<QuestionDto, Question>(funcMain1),
                SubjectId = p1.SubjectId,
                Id = p1.Id
            };
        }
        public static Topic AdaptTo(this TopicDto p3, Topic p4)
        {
            if (p3 == null)
            {
                return null;
            }
            Topic result = p4 ?? new Topic();
            
            result.Name = p3.Name;
            result.TopicNumber = p3.TopicNumber;
            result.Questions = p3.Questions == null ? null : p3.Questions.Select<QuestionDto, Question>(funcMain2);
            result.SubjectId = p3.SubjectId;
            result.Id = p3.Id;
            return result;
            
        }
        public static Expression<Func<TopicDto, Topic>> ProjectToTopic => p6 => new Topic()
        {
            Name = p6.Name,
            TopicNumber = p6.TopicNumber,
            Questions = p6.Questions.Select<QuestionDto, Question>(p7 => new Question()
            {
                QuestionNumber = p7.QuestionNumber,
                TopicId = p7.TopicId,
                Id = p7.Id
            }),
            SubjectId = p6.SubjectId,
            Id = p6.Id
        };
        public static TopicDto AdaptToDto(this Topic p8)
        {
            return p8 == null ? null : new TopicDto()
            {
                Name = p8.Name,
                TopicNumber = p8.TopicNumber,
                Questions = p8.Questions == null ? null : p8.Questions.Select<Question, QuestionDto>(funcMain3),
                SubjectId = p8.SubjectId,
                Id = p8.Id
            };
        }
        public static TopicDto AdaptTo(this Topic p10, TopicDto p11)
        {
            if (p10 == null)
            {
                return null;
            }
            TopicDto result = p11 ?? new TopicDto();
            
            result.Name = p10.Name;
            result.TopicNumber = p10.TopicNumber;
            result.Questions = p10.Questions == null ? null : p10.Questions.Select<Question, QuestionDto>(funcMain4);
            result.SubjectId = p10.SubjectId;
            result.Id = p10.Id;
            return result;
            
        }
        public static Expression<Func<Topic, TopicDto>> ProjectToDto => p13 => new TopicDto()
        {
            Name = p13.Name,
            TopicNumber = p13.TopicNumber,
            Questions = p13.Questions.Select<Question, QuestionDto>(p14 => new QuestionDto()
            {
                QuestionNumber = p14.QuestionNumber,
                TopicId = p14.TopicId,
                Id = p14.Id
            }),
            SubjectId = p13.SubjectId,
            Id = p13.Id
        };
        
        private static Question funcMain1(QuestionDto p2)
        {
            return p2 == null ? null : new Question()
            {
                QuestionNumber = p2.QuestionNumber,
                TopicId = p2.TopicId,
                Id = p2.Id
            };
        }
        
        private static Question funcMain2(QuestionDto p5)
        {
            return p5 == null ? null : new Question()
            {
                QuestionNumber = p5.QuestionNumber,
                TopicId = p5.TopicId,
                Id = p5.Id
            };
        }
        
        private static QuestionDto funcMain3(Question p9)
        {
            return p9 == null ? null : new QuestionDto()
            {
                QuestionNumber = p9.QuestionNumber,
                TopicId = p9.TopicId,
                Id = p9.Id
            };
        }
        
        private static QuestionDto funcMain4(Question p12)
        {
            return p12 == null ? null : new QuestionDto()
            {
                QuestionNumber = p12.QuestionNumber,
                TopicId = p12.TopicId,
                Id = p12.Id
            };
        }
    }
}