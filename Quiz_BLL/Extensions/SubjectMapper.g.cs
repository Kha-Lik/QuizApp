using System;
using System.Linq;
using System.Linq.Expressions;
using Quiz_BLL.Models;
using Quiz_DAL.Entities;

namespace Quiz_BLL.Extensions
{
    public static partial class SubjectMapper
    {
        public static Subject AdaptToSubject(this SubjectDto p1)
        {
            return p1 == null ? null : new Subject()
            {
                Name = p1.Name,
                LecturerId = p1.LecturerId,
                Topics = p1.Topics == null ? null : p1.Topics.Select<TopicDto, Topic>(funcMain1),
                Id = p1.Id
            };
        }
        public static Subject AdaptTo(this SubjectDto p4, Subject p5)
        {
            if (p4 == null)
            {
                return null;
            }
            Subject result = p5 ?? new Subject();
            
            result.Name = p4.Name;
            result.LecturerId = p4.LecturerId;
            result.Topics = p4.Topics == null ? null : p4.Topics.Select<TopicDto, Topic>(funcMain3);
            result.Id = p4.Id;
            return result;
            
        }
        public static Expression<Func<SubjectDto, Subject>> ProjectToSubject => p8 => new Subject()
        {
            Name = p8.Name,
            LecturerId = p8.LecturerId,
            Topics = p8.Topics.Select<TopicDto, Topic>(p9 => new Topic()
            {
                Name = p9.Name,
                TopicNumber = p9.TopicNumber,
                Questions = p9.Questions.Select<QuestionDto, Question>(p10 => new Question()
                {
                    QuestionNumber = p10.QuestionNumber,
                    TopicId = p10.TopicId,
                    Id = p10.Id
                }),
                SubjectId = p9.SubjectId,
                Id = p9.Id
            }),
            Id = p8.Id
        };
        public static SubjectDto AdaptToDto(this Subject p11)
        {
            return p11 == null ? null : new SubjectDto()
            {
                Name = p11.Name,
                LecturerId = p11.LecturerId,
                Topics = p11.Topics == null ? null : p11.Topics.Select<Topic, TopicDto>(funcMain5),
                Id = p11.Id
            };
        }
        public static SubjectDto AdaptTo(this Subject p14, SubjectDto p15)
        {
            if (p14 == null)
            {
                return null;
            }
            SubjectDto result = p15 ?? new SubjectDto();
            
            result.Name = p14.Name;
            result.LecturerId = p14.LecturerId;
            result.Topics = p14.Topics == null ? null : p14.Topics.Select<Topic, TopicDto>(funcMain7);
            result.Id = p14.Id;
            return result;
            
        }
        public static Expression<Func<Subject, SubjectDto>> ProjectToDto => p18 => new SubjectDto()
        {
            Name = p18.Name,
            LecturerId = p18.LecturerId,
            Topics = p18.Topics.Select<Topic, TopicDto>(p19 => new TopicDto()
            {
                Name = p19.Name,
                TopicNumber = p19.TopicNumber,
                Questions = p19.Questions.Select<Question, QuestionDto>(p20 => new QuestionDto()
                {
                    QuestionNumber = p20.QuestionNumber,
                    TopicId = p20.TopicId,
                    Id = p20.Id
                }),
                SubjectId = p19.SubjectId,
                Id = p19.Id
            }),
            Id = p18.Id
        };
        
        private static Topic funcMain1(TopicDto p2)
        {
            return p2 == null ? null : new Topic()
            {
                Name = p2.Name,
                TopicNumber = p2.TopicNumber,
                Questions = p2.Questions == null ? null : p2.Questions.Select<QuestionDto, Question>(funcMain2),
                SubjectId = p2.SubjectId,
                Id = p2.Id
            };
        }
        
        private static Topic funcMain3(TopicDto p6)
        {
            return p6 == null ? null : new Topic()
            {
                Name = p6.Name,
                TopicNumber = p6.TopicNumber,
                Questions = p6.Questions == null ? null : p6.Questions.Select<QuestionDto, Question>(funcMain4),
                SubjectId = p6.SubjectId,
                Id = p6.Id
            };
        }
        
        private static TopicDto funcMain5(Topic p12)
        {
            return p12 == null ? null : new TopicDto()
            {
                Name = p12.Name,
                TopicNumber = p12.TopicNumber,
                Questions = p12.Questions == null ? null : p12.Questions.Select<Question, QuestionDto>(funcMain6),
                SubjectId = p12.SubjectId,
                Id = p12.Id
            };
        }
        
        private static TopicDto funcMain7(Topic p16)
        {
            return p16 == null ? null : new TopicDto()
            {
                Name = p16.Name,
                TopicNumber = p16.TopicNumber,
                Questions = p16.Questions == null ? null : p16.Questions.Select<Question, QuestionDto>(funcMain8),
                SubjectId = p16.SubjectId,
                Id = p16.Id
            };
        }
        
        private static Question funcMain2(QuestionDto p3)
        {
            return p3 == null ? null : new Question()
            {
                QuestionNumber = p3.QuestionNumber,
                TopicId = p3.TopicId,
                Id = p3.Id
            };
        }
        
        private static Question funcMain4(QuestionDto p7)
        {
            return p7 == null ? null : new Question()
            {
                QuestionNumber = p7.QuestionNumber,
                TopicId = p7.TopicId,
                Id = p7.Id
            };
        }
        
        private static QuestionDto funcMain6(Question p13)
        {
            return p13 == null ? null : new QuestionDto()
            {
                QuestionNumber = p13.QuestionNumber,
                TopicId = p13.TopicId,
                Id = p13.Id
            };
        }
        
        private static QuestionDto funcMain8(Question p17)
        {
            return p17 == null ? null : new QuestionDto()
            {
                QuestionNumber = p17.QuestionNumber,
                TopicId = p17.TopicId,
                Id = p17.Id
            };
        }
    }
}