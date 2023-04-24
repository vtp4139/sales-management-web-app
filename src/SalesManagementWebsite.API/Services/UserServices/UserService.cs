using AutoMapper;
using SalesManagementWebsite.API.Services.HashServices;
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
                var userLogin = await _unitOfWork.UserRepository.GetAsync(u => u.UserName.Equals(userLoginDto.UserName)); 

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

                //Hash password input and then compare
                bool comparePass = userLogin.Password.Equals(HashPasswordsHelper.HashPasswords(userLoginDto.Password, userLogin.Salt));

                if (!comparePass)
                {
                    return new ResponseHandle<UserOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = "Password is wrong, please try a new password!"
                };
            }

                //Gen JWT Token
                var token = TokenHelper.GenerateToken(
                      _configuration["JWT:Secret"]
                      , _configuration["JWT:ValidIssuer"]
                      , _configuration["JWT:ValidAudience"]
                      , userLogin.UserRoles.Select(ur => ur.Roles.Name).ToList()
                      , userLogin.Id.ToString()
                      , userLogin.UserName
                      , userLogin.Name); ;


                var roleList = userLogin.UserRoles.Select(ur => ur.Roles).ToList();

                var roleListOutput = _mapper.Map<List<Role>, List<UserRoleDto>>(roleList);
                var userOutput = _mapper.Map<User, UserOuputDto>(userLogin);
                userOutput.Roles.AddRange(roleListOutput);

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

                //Hash password when save to db
                user.Salt = HashPasswordsHelper.GeneratedSalt();
                user.Password = HashPasswordsHelper.HashPasswords(user.Password, user.Salt);

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
