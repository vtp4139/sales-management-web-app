using AutoMapper;
using SalesManagementWebsite.API.Services.JWTServices;
using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;

namespace SalesManagementWebsite.API.Services.UserServices
{
    public class UserService : IUserService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<UserOuputDto> Login(UserLoginDto userLoginDto)
        {
            var user = _mapper.Map<UserLoginDto, User>(userLoginDto);

            var userLogin = await _unitOfWork.UserRepository.GetAsync(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password));

            if (userLogin == null)
            {
                throw new Exception("Can not find user!");
            }

            var token = TokenHelper.GenerateToken(
          _configuration["JWT:Secret"]
          , _configuration["JWT:ValidIssuer"]
          , _configuration["JWT:ValidAudience"]
          , null
          , user.Id.ToString()
          , user.UserName
          , user.Name);

            return new UserOuputDto
            {
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                Phone = user.Phone,
                Token = token
              };
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
