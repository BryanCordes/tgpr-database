using AutoMapper;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.DataAccess.Entities.Applications;

namespace TGPR.Database.Components.Mapping.Profiles.Applications
{
    internal class ApplicationQuestionProfile : Profile
    {
        public ApplicationQuestionProfile()
        {
            CreateMap<ApplicationQuestion, ApplicationQuestionModel>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();
        }
    }
}
