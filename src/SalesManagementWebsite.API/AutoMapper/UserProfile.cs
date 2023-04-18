using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.API.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserRegisterDto, User>();
        }
    }
}
