using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Dtos.Supplier;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.API.Services.SupplierServices
{
    public class SupplierServices : ISupplierServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public SupplierServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<SupplierServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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
                        Data = null,
                        ErrorMessage = $"Can not get list of [Supplier]"
                    };
                }

                var supplierListOutput = _mapper.Map<List<SupplierOutputDto>>(gSupplierList);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = null,
                    ListData = supplierListOutput,
                    ErrorMessage = string.Empty
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
                        Data = null,
                        ErrorMessage = $"Can not get [Supplier] with [id]: {id}"
                    };
                }

                var supplierOutput = _mapper.Map<SupplierOutputDto>(gSupplier);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = supplierOutput,
                    ErrorMessage = string.Empty
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

                _unitOfWork.SupplierRepository.Add(supplier);
                await _unitOfWork.CommitAsync();

                var supplierOutput = _mapper.Map<SupplierOutputDto>(supplier);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = supplierOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"SupplierServices -> CreateSupplier({JsonSerializer.Serialize(supplierCreateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<SupplierOutputDto>> UpdateSupplier(SupplierUpdateDto supplierUpdateDto)
        {
            try
            {
                var gSupplier = await _unitOfWork.SupplierRepository.GetAsync(c => c.Id.Equals(supplierUpdateDto.Id));

                if (gSupplier == null)
                {
                    return new ResponseHandle<SupplierOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get the [Supplier]: {JsonSerializer.Serialize(supplierUpdateDto)}"
                    };
                }

                //Mapping field modify
                gSupplier.CompanyName = supplierUpdateDto.CompanyName;
                gSupplier.Address = supplierUpdateDto.Address;
                gSupplier.Phone = supplierUpdateDto.Phone;
                gSupplier.City = supplierUpdateDto.City;

                _unitOfWork.SupplierRepository.Update(gSupplier);
                await _unitOfWork.CommitAsync();

                var supplierOutput = _mapper.Map<SupplierOutputDto>(gSupplier);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = supplierOutput,
                    ErrorMessage = string.Empty
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
                        Data = null,
                        ErrorMessage = $"Can not get [Supplier] with [id]: {id}"
                    };
                }

                _unitOfWork.SupplierRepository.Remove(gSupplier);
                await _unitOfWork.CommitAsync();


                var cateOutput = _mapper.Map<SupplierOutputDto>(gSupplier);

                return new ResponseHandle<SupplierOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = cateOutput,
                    ErrorMessage = string.Empty
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
