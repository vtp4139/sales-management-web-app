using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;

namespace SalesManagementWebsite.Core.Services.UserServices
{
    public interface IUserService
    {
        public ValueTask<ResponseHandle<UserOuputDto>> Login(UserLoginDto userRegisterDto);
        public ValueTask<ResponseHandle<UserOuputDto>> Register(UserRegisterDto userRegisterDto);
        public ValueTask<ResponseHandle<UserOuputDto>> GetUserByUserName(string userName);
        public ValueTask<ResponseHandle<UsersListOuputDto>> GetAllUsers();
        public ValueTask<ResponseHandle<UserOuputDto>> UpdateUser(string userName, UserInputDto userInputDto);
        public ValueTask<ResponseHandle<UserOuputDto>> ChangeStatusUser(string userName, UserStatusInputDto userInputDto);
    }
}
