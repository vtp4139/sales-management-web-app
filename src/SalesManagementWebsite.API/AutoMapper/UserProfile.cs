using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.API.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserLoginDto, User>();

            CreateMap<UserRegisterDto, User>();

            CreateMap<User, UserOuputDto>();

            CreateMap<Role, UserRoleDto>();
        }
    }
}
