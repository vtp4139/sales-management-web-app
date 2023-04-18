using AutoMapper;
using SalesManagementWebsite.API.Services.JWTServices;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;

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

        public async Task<ResponseHandle<UserOuputDto>> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var user = _mapper.Map<UserLoginDto, User>(userLoginDto);

                var userLogin = await _unitOfWork.UserRepository.GetAsync(u => u.UserName.Equals(user.UserName) && u.Password.Equals(user.Password));

                if (userLogin == null)
                {
                    return new ResponseHandle<UserOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = "User not found, login fail"
                    };
                }

                var token = TokenHelper.GenerateToken(
              _configuration["JWT:Secret"]
              , _configuration["JWT:ValidIssuer"]
              , _configuration["JWT:ValidAudience"]
              , null
              , user.Id.ToString()
              , user.UserName
              , user.Name);

                var userOutput = _mapper.Map<User, UserOuputDto>(userLogin);

                //Get token
                userOutput.Token = token;

                return new ResponseHandle<UserOuputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = userOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseHandle<UserOuputDto>> Register(UserRegisterDto registerDto)
        {
            try
            {
                var user = _mapper.Map<UserRegisterDto, User>(registerDto);

                _unitOfWork.UserRepository.Add(user);
                await _unitOfWork.CommitAsync();

                var userOutput = _mapper.Map<User, UserOuputDto>(user);

                return new ResponseHandle<UserOuputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = userOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
