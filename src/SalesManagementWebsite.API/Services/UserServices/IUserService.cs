using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.API.Services.UserServices
{
    public interface IUserService
    {
        public ValueTask<ResponseHandle<UserOuputDto>> Login(UserLoginDto userRegisterDto);
        public ValueTask<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto);
        public ValueTask<ResponseHandle<UserOuputDto>> GetUser(string userName);
        public ValueTask<ResponseHandle<UsersListOuputDto>> GetAllUsers();
        public ValueTask<ResponseHandle<UserOuputDto>> UpdateUser(UserInputDto userInputDto);
        public ValueTask<ResponseHandle<UserOuputDto>> ChangeStatusUser(UserStatusInputDto userInputDto);
    }
}
