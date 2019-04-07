using AutoMapper;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.DataAccess.Entities.Applications;

namespace TGPR.Database.Components.Mapping.Profiles.Applications
{
    internal class ApplicationOptionProfile : Profile
    {
        public ApplicationOptionProfile()
        {
            CreateMap<ApplicationOption, ApplicationOptionModel>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();
        }
    }
}
