using AutoMapper;
using TGPR.Database.Common.Models.Users;
using TGPR.Database.DataAccess.Entities.Users;

namespace TGPR.Database.Components.Mapping.Profiles.Users
{
    internal class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleModel>()
                .IgnoreAllNonExisting()
                .ReverseMap()
                .IgnoreAllNonExisting();
        }
    }
}
