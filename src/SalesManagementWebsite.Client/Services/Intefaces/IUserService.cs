using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.Client.Services.Intefaces
{
    public interface IUserService
    {
        Task<ResponseHandle<UserOuputDto>> Login(UserLoginDto userLoginDto);
        Task<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto);
    }
}
