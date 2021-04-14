using System;
using System.Linq.Expressions;
using QuizApp.Business.Models;
using QuizApp.DataAccess.Entities;

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
                TimeToPass = p1.TimeToPass,
                QuestionsPerAttempt = p1.QuestionsPerAttempt,
                MaxAttemptCount = p1.MaxAttemptCount,
                SubjectId = p1.SubjectId,
                Id = p1.Id
            };
        }
        public static Topic AdaptTo(this TopicDto p2, Topic p3)
        {
            if (p2 == null)
            {
                return null;
            }
            Topic result = p3 ?? new Topic();
            
            result.Name = p2.Name;
            result.TopicNumber = p2.TopicNumber;
            result.TimeToPass = p2.TimeToPass;
            result.QuestionsPerAttempt = p2.QuestionsPerAttempt;
            result.MaxAttemptCount = p2.MaxAttemptCount;
            result.SubjectId = p2.SubjectId;
            result.Id = p2.Id;
            return result;
            
        }
        public static Expression<Func<TopicDto, Topic>> ProjectToTopic => p4 => new Topic()
        {
            Name = p4.Name,
            TopicNumber = p4.TopicNumber,
            TimeToPass = p4.TimeToPass,
            QuestionsPerAttempt = p4.QuestionsPerAttempt,
            MaxAttemptCount = p4.MaxAttemptCount,
            SubjectId = p4.SubjectId,
            Id = p4.Id
        };
        public static TopicDto AdaptToDto(this Topic p5)
        {
            return p5 == null ? null : new TopicDto()
            {
                Name = p5.Name,
                TopicNumber = p5.TopicNumber,
                TimeToPass = p5.TimeToPass,
                QuestionsPerAttempt = p5.QuestionsPerAttempt,
                MaxAttemptCount = p5.MaxAttemptCount,
                SubjectId = p5.SubjectId,
                Id = p5.Id
            };
        }
        public static TopicDto AdaptTo(this Topic p6, TopicDto p7)
        {
            if (p6 == null)
            {
                return null;
            }
            TopicDto result = p7 ?? new TopicDto();
            
            result.Name = p6.Name;
            result.TopicNumber = p6.TopicNumber;
            result.TimeToPass = p6.TimeToPass;
            result.QuestionsPerAttempt = p6.QuestionsPerAttempt;
            result.MaxAttemptCount = p6.MaxAttemptCount;
            result.SubjectId = p6.SubjectId;
            result.Id = p6.Id;
            return result;
            
        }
        public static Expression<Func<Topic, TopicDto>> ProjectToDto => p8 => new TopicDto()
        {
            Name = p8.Name,
            TopicNumber = p8.TopicNumber,
            TimeToPass = p8.TimeToPass,
            QuestionsPerAttempt = p8.QuestionsPerAttempt,
            MaxAttemptCount = p8.MaxAttemptCount,
            SubjectId = p8.SubjectId,
            Id = p8.Id
        };
    }
}