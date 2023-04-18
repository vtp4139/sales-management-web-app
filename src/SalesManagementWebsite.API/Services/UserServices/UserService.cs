using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;

namespace SalesManagementWebsite.API.Services.UserServices
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserOuputDto> Register(UserRegisterDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.UserName,
                Password = registerDto.Password,
                Email = registerDto.Email,
                Phone = registerDto.Phone,
                Address = registerDto.Address,
            };

            _unitOfWork.UserRepository.Add(user);
            await _unitOfWork.CommitAsync();

            return new UserOuputDto
            {
                UserName = registerDto.UserName,
                Password = registerDto.Password,
                Email = registerDto.Email,
                Phone = registerDto.Phone
            };
        }
    }
}
