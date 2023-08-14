using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Role;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Core.AutoMapper
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleOutputDto>();
            CreateMap<RoleInputDto, Role>();
            CreateMap<RoleUpdateDto, Role>();
        }
    }
}
