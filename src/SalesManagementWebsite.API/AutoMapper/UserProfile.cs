using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Core.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserLoginDto, User>();

            CreateMap<UserInputDto, User>();

            CreateMap<User, UserOuputDto>();

            CreateMap<Role, UserRoleDto>();

            CreateMap<User, UsersListOuputDto>();

            CreateMap<UserRegisterDto, User>();
        }
    }
}
