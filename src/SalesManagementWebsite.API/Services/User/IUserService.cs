using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.API.Services.User
{
    public interface IUserService
    {
        public Task Register(UserRegisterDto userRegisterDto);
    }
}
