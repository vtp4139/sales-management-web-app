using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.API.Services.UserServices
{
    public interface IUserService
    {
        public Task<UserOuputDto> Login(UserLoginDto userRegisterDto);
        public Task<UserOuputDto> Register(UserRegisterDto userRegisterDto);

    }
}
