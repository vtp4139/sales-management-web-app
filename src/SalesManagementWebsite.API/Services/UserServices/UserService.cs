using AutoMapper;
using SalesManagementWebsite.API.Services.HashServices;
using SalesManagementWebsite.API.Services.JWTServices;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.User;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.Enums;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Data;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.API.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async ValueTask<ResponseHandle<UserOuputDto>> Login(UserLoginDto userLoginDto)
        {
            try
            {
                var userLogin = await _unitOfWork.UserRepository.GetUser(userLoginDto.UserName);

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
                      , userLogin.UserRoles.Select(ur => ur.Role.Name).ToList()
                      , userLogin.Id.ToString()
                      , userLogin.UserName
                      , userLogin.Name); ;


                var roleList = userLogin.UserRoles.Select(ur => ur.Role).ToList();

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
                _logger.LogError($"UserService -> Login({JsonSerializer.Serialize(userLoginDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<UserOuputDto>> Register(UserRegisterDto registerDto)
        {
            try
            {
                var user = _mapper.Map<User>(registerDto);

                //Hash password when save to db
                user.Salt = HashPasswordsHelper.GeneratedSalt();
                user.Password = HashPasswordsHelper.HashPasswords(user.Password, user.Salt);
                _unitOfWork.UserRepository.Add(user);
              
                //Add roles for user
                foreach (var role in registerDto.Roles)
                {
                    _unitOfWork.UserRoleRepository.Add(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role
                    });
                }
                await _unitOfWork.CommitAsync();

                var userOutput = _mapper.Map<UserOuputDto>(user);

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
                _logger.LogError($"UserService -> Register({JsonSerializer.Serialize(registerDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<UserOuputDto>> GetUser(string userName)
        {
            try
            {
                var userLogin = await _unitOfWork.UserRepository.GetAsync(u => u.UserName.Equals(userName));

                if (userLogin == null)
                {
                    return new ResponseHandle<UserOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get the info of user: {userName}"
                    };
                }

                //Get roles of uer
                var roleList = userLogin.UserRoles.Select(ur => ur.Role).ToList();
                var roleListOutput = _mapper.Map<List<Role>, List<UserRoleDto>>(roleList);

                //Map user and role to dto
                var userOutput = _mapper.Map<User, UserOuputDto>(userLogin);
                userOutput.Roles.AddRange(roleListOutput);

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
                _logger.LogError($"UserService -> GetUser({userName}) - Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<UsersListOuputDto>> GetAllUsers()
        {
            try
            {
                var userList = await _unitOfWork.UserRepository.GetAllAsync();

                if (userList == null)
                {
                    return new ResponseHandle<UsersListOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get the list of user"
                    };
                }

                var userListOutput = _mapper.Map<List<User>, List<UsersListOuputDto>>(userList.ToList());

                return new ResponseHandle<UsersListOuputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = null,
                    ListData = userListOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserService -> GetAllUsers - Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<UserOuputDto>> UpdateUser(UserInputDto userInputDto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUser(userInputDto.UserName);

                if (user == null)
                {
                    return new ResponseHandle<UserOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get the info of [User]: {userInputDto.UserName}"
                    };
                }

                //Mapping field modify
                user.Name = userInputDto.Name;
                user.Phone = userInputDto.Phone;
                user.Address = userInputDto.Address;
                user.Email = userInputDto.Email;
                user.IdentityCard = userInputDto.IdentityCard;
                user.DOB = userInputDto.DOB;
                user.ModifiedDate = DateTime.Now;

                _unitOfWork.UserRepository.Update(user);
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
                _logger.LogError($"UserService -> Register({JsonSerializer.Serialize(userInputDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<UserOuputDto>> ChangeStatusUser(UserStatusInputDto userInputDto)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUser(userInputDto.UserName);

                if (user == null)
                {
                    return new ResponseHandle<UserOuputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get the info of user: {userInputDto.UserName}"
                    };
                }

                //Mapping field modify
                user.UserStatus = (UserStatus)userInputDto.StatusUser; //-> Change status

                _unitOfWork.UserRepository.Update(user);
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
                _logger.LogError($"UserService -> ChangeStatusUser({JsonSerializer.Serialize(userInputDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
