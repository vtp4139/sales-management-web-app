using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Role;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.Core.Services.RoleServices
{
    public class RoleServices : IRoleServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public RoleServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RoleServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async ValueTask<ResponseHandle<RoleOutputDto>> GetAllRoles()
        {
            try
            {
                var roles = await _unitOfWork.RoleRepository.GetAllAsync();

                if (roles == null)
                {
                    return new ResponseHandle<RoleOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get list [Roles]"
                    };
                }

                var rolesOutput = _mapper.Map<List<RoleOutputDto>>(roles);

                return new ResponseHandle<RoleOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = null,
                    ListData = rolesOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"RoleServices -> GetAllRoles() " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<RoleOutputDto>> GetRole(Guid id)
        {
            try
            {
                var role = await _unitOfWork.RoleRepository.GetAsync(c => c.Id.Equals(id));

                if (role == null)
                {
                    return new ResponseHandle<RoleOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get [Role] with [id]: {id}"
                    };
                }

                var roleOutput = _mapper.Map<RoleOutputDto>(role);

                return new ResponseHandle<RoleOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = roleOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"RoleServices -> GetRole({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<RoleOutputDto>> CreateRole(RoleInputDto roleInputDto)
        {
            try
            {
                var role = _mapper.Map<Role>(roleInputDto);

                _unitOfWork.RoleRepository.Add(role);
                await _unitOfWork.CommitAsync();

                var roleOutput = _mapper.Map<RoleOutputDto>(role);

                return new ResponseHandle<RoleOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = roleOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"RoleServices -> CreateRole({JsonSerializer.Serialize(roleInputDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<RoleOutputDto>> UpdateRole(RoleUpdateDto roleUpdateDto)
        {
            try
            {
                var role = await _unitOfWork.RoleRepository.GetAsync(c => c.Id.Equals(roleUpdateDto.Id));

                if (role == null)
                {
                    return new ResponseHandle<RoleOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get the [Role]: {JsonSerializer.Serialize(roleUpdateDto)}"
                    };
                }

                //Mapping field modify
                role.Name = roleUpdateDto.Name;
                role.Description = roleUpdateDto.Description;

                _unitOfWork.RoleRepository.Update(role);
                await _unitOfWork.CommitAsync();

                var roleOutput = _mapper.Map<RoleOutputDto>(role);

                return new ResponseHandle<RoleOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = roleOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"RoleServices -> UpdateRole({JsonSerializer.Serialize(roleUpdateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<RoleOutputDto>> DeleteRole(Guid id)
        {
            try
            {
                var role = await _unitOfWork.RoleRepository.GetAsync(c => c.Id.Equals(id));

                if (role == null)
                {
                    return new ResponseHandle<RoleOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get [Role] with [id]: {id}"
                    };
                }

                _unitOfWork.RoleRepository.Remove(role);
                await _unitOfWork.CommitAsync();


                var roleOutput = _mapper.Map<RoleOutputDto>(role);

                return new ResponseHandle<RoleOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = roleOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"RoleServices -> {{({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
