using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.DataAccess.Entities.Applications;

namespace TGPR.Database.Components.Mapping.Profiles.Applications
{
    internal class ApplicationTemplateProfile : Profile
    {
        public ApplicationTemplateProfile()
        {
            CreateMap<ApplicationTemplate, ApplicationTemplateModel>()
                .IgnoreAllNonExisting()
                .AfterMap((entity, model) => AssignChildQuestions(model));

            CreateMap<ApplicationTemplateModel, ApplicationTemplate>()
                .IgnoreAllNonExisting();
        }

        private void AssignChildQuestions(ApplicationTemplateModel template)
        {
            Dictionary<int, List<ApplicationQuestionModel>> childQuestions = template.Categories
                .SelectMany(x => x.Questions)
                .Where(x => x.ParentApplicationOptionId != null)
                .GroupBy(x => (int)x.ParentApplicationOptionId)
                .ToDictionary(x => x.Key, x => x.ToList());

            IEnumerable<ApplicationOptionModel> options = template.Categories
                .SelectMany(x => x.Questions)
                .SelectMany(x => x.Options)
                .Where(x => childQuestions.ContainsKey(x.ApplicationOptionId));
            foreach (var option in options)
            {
                option.ChildQuestions = childQuestions[option.ApplicationOptionId];
            }
        }
    }
}
