using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;

namespace SalesManagementWebsite.API.Services.UserServices
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;    

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserOuputDto> Register(UserRegisterDto registerDto)
        {
            var user = _mapper.Map<UserRegisterDto, User>(registerDto);

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
