using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.Serialization;
using Mapster;
using QuizApp.DataAccess.Entities;

namespace QuizApp.Business.MapsterConfig
{
    public class MappingRegister : ICodeGenerationRegister
    {
        public void Register(CodeGenerationConfig config)
        {
            config.AdaptTwoWays("[name]Dto", MapType.Map | MapType.MapToTarget | MapType.Projection)
                .ApplyDefaultRule();
            
            config.AdaptTwoWays("[name]Dto", MapType.Map | MapType.MapToTarget | MapType.Projection)
                .ForType<User>().IgnoreNoAttributes(typeof(DataMemberAttribute));

            config.GenerateMapper("[name]Mapper")
                .ForAllTypesInNamespace(Assembly.GetAssembly(typeof(BaseEntity)), "QuizApp.DataAccess.Entities")
                .ExcludeTypes(typeof(User));

        }
    }

    internal static class RegisterExtensions
    {
        public static AdaptAttributeBuilder ApplyDefaultRule(this AdaptAttributeBuilder builder)
        {
            return builder
                .ForAllTypesInNamespace(Assembly.GetAssembly(typeof(BaseEntity)), "QuizApp.DataAccess.Entities")
                .ExcludeTypes(typeof(BaseEntity), typeof(User))
                .ShallowCopyForSameType(true)
                .ForType<Subject>(cfg => cfg.Ignore(s => s.Lecturer))
                .ForType<Topic>(cfg => cfg.Ignore(t => t.Subject))
                .ForType<Question>(cfg => cfg.Ignore(q => q.Topic))
                .ForType<Answer>(cfg => cfg.Ignore(a => a.Question))
                .ForType<Attempt>(cfg => cfg.Ignore(at => at.Student)
                    .Ignore(a => a.Topic))
                .ForType<QuestionResult>(cfg => cfg.Ignore(qr => qr.Attempt)
                    .Ignore(qr => qr.Question));
        }
    }
}