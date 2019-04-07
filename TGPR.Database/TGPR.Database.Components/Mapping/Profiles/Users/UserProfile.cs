using AutoMapper;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.DataAccess.Entities.Users;

namespace TGPR.Database.Components.Mapping.Profiles.Users
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserModel>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();

            CreateMap<User, UserSummaryModel>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();
        }
    }
}
