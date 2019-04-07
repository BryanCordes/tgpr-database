using AutoMapper;
using TGPR.Database.Common.Models.Applications;
using TGPR.Database.DataAccess.Entities.Applications;

namespace TGPR.Database.Components.Mapping.Profiles.Applications
{
    internal class ApplicationCategoryProfile : Profile
    {
        public ApplicationCategoryProfile()
        {
            CreateMap<ApplicationCategory, ApplicationCategoryModel>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();
        }
    }
}
