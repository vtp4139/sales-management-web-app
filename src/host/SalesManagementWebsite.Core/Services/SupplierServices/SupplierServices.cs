using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Supplier;
using SalesManagementWebsite.Contracts.Utilities;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.Core.Services.SupplierServices
{
    public class SupplierServices : ISupplierServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SupplierServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SupplierServices> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async ValueTask<ResponseHandle<SupplierOutputDto>> GetAllSuppliers()
        {
            try
            {
                var gSupplierList = await _unitOfWork.SupplierRepository.GetAllAsync();

                if (gSupplierList == null)
                {
                    return new ResponseHandle<SupplierOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_LIST, nameof(Supplier))
                    };
                }

                var supplierListOutput = _mapper.Map<List<SupplierOutputDto>>(gSupplierList);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    ListData = supplierListOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"SupplierServices -> GetAllSuppliers() " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<SupplierOutputDto>> GetSupplier(Guid id)
        {
            try
            {
                var gSupplier = await _unitOfWork.SupplierRepository.GetAsync(s => s.Id.Equals(id));

                if (gSupplier == null)
                {
                    return new ResponseHandle<SupplierOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Supplier), id)
                    };
                }

                var supplierOutput = _mapper.Map<SupplierOutputDto>(gSupplier);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = supplierOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"SupplierServices -> GetSupplier({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<SupplierOutputDto>> CreateSupplier(SupplierCreateDto supplierCreateDto)
        {
            try
            {
                var supplier = _mapper.Map<Supplier>(supplierCreateDto);

                supplier.CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;

                _unitOfWork.SupplierRepository.Add(supplier);
                await _unitOfWork.CommitAsync();

                var supplierOutput = _mapper.Map<SupplierOutputDto>(supplier);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = supplierOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"SupplierServices -> CreateSupplier({JsonSerializer.Serialize(supplierCreateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<SupplierOutputDto>> UpdateSupplier(Guid id, SupplierUpdateDto supplierUpdateDto)
        {
            try
            {
                var supplier = await _unitOfWork.SupplierRepository.GetAsync(c => c.Id.Equals(id));

                if (supplier == null)
                {
                    return new ResponseHandle<SupplierOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Supplier), id)
                    };
                }

                //Mapping field modify
                supplier.CompanyName = supplierUpdateDto.CompanyName;
                supplier.Address = supplierUpdateDto.Address;
                supplier.Phone = supplierUpdateDto.Phone;
                supplier.City = supplierUpdateDto.City;
                supplier.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;
                supplier.ModifiedDate = DateTime.Now;

                _unitOfWork.SupplierRepository.Update(supplier);
                await _unitOfWork.CommitAsync();

                var supplierOutput = _mapper.Map<SupplierOutputDto>(supplier);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = supplierOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"SupplierServices -> UpdateSupplier({JsonSerializer.Serialize(supplierUpdateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<SupplierOutputDto>> DeleteSupplier(Guid id)
        {
            try
            {
                var gSupplier = await _unitOfWork.SupplierRepository.GetAsync(c => c.Id.Equals(id));

                if (gSupplier == null)
                {
                    return new ResponseHandle<SupplierOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Supplier), id)
                    };
                }

                _unitOfWork.SupplierRepository.Remove(gSupplier);
                await _unitOfWork.CommitAsync();


                var cateOutput = _mapper.Map<SupplierOutputDto>(gSupplier);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = cateOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"SupplierServices -> DeleteSupplier({id}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
