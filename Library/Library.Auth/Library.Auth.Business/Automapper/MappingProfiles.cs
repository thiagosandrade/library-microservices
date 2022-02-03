using System.Collections.Generic;
using AutoMapper;
using Library.Auth.Business.CQRS.Contracts.Queries;
using Library.Auth.Domain.Models;

namespace Library.Auth.Business.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<User, GetUserQueryResult>();

            CreateMap<List<User>, GetAllUserQueryResult>()
                .ForMember(map => map.Users,
                    opt => opt.MapFrom(x => x));

            CreateMap<UserRole, UserRolesResult>()
                .ForMember(map => map.UserRole,
                    opt => opt.MapFrom(x => x.Role.RoleName));

            CreateMap<User, GetUserWithRolesQueryResult>();

            CreateMap<List<User>, GetAllUserWithRolesQueryResult>()
                .ForMember(map => map.Users,
                    opt => opt.MapFrom(x => x));
        }
    }
}
