using API.Application.Permissions.DTO;
using API.Application.Users.DTO;
using API.Domain.Entities;
using AutoMapper;

namespace API.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, GetUserDTO>().ReverseMap();
            CreateMap<Permission, PermissionDTO>().ReverseMap();
        }
    }
}
