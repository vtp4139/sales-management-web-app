using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.Core.Services.BrandServices
{
    public class BrandServices : IBrandServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BrandServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BrandServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async ValueTask<ResponseHandle<BrandOutputDto>> GetAllBrands()
        {
            try
            {
                var gBrandList = await _unitOfWork.BrandRepository.GetAllAsync();

                if (gBrandList == null)
                {
                    return new ResponseHandle<BrandOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get list [Brands]"
                    };
                }

                var brandListOutput = _mapper.Map<List<BrandOutputDto>>(gBrandList);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = null,
                    ListData = brandListOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrandServices -> GetAllBrands() " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<BrandOutputDto>> GetBrand(Guid id)
        {
            try
            {
                var gBrand = await _unitOfWork.BrandRepository.GetAsync(c => c.Id.Equals(id));

                if (gBrand == null)
                {
                    return new ResponseHandle<BrandOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get [Brand] with [id]: {id}"
                    };
                }

                var cateOutput = _mapper.Map<BrandOutputDto>(gBrand);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = cateOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrandServices -> GetBrand({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<BrandOutputDto>> CreateBrand(BrandCreateDto brandCreateDto)
        {
            try
            {
                var brand = _mapper.Map<Brand>(brandCreateDto);

                _unitOfWork.BrandRepository.Add(brand);
                await _unitOfWork.CommitAsync();

                var brandOutput = _mapper.Map<BrandOutputDto>(brand);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = brandOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrandServices -> CreateBrand({JsonSerializer.Serialize(brandCreateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<BrandOutputDto>> UpdateBrand(BrandInputDto BrandInputDto)
        {
            try
            {
                var gBrand = await _unitOfWork.BrandRepository.GetAsync(c => c.Id.Equals(BrandInputDto.Id));

                if (gBrand == null)
                {
                    return new ResponseHandle<BrandOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get the [Brand]: {JsonSerializer.Serialize(BrandInputDto)}"
                    };
                }

                //Mapping field modify
                gBrand.Name = BrandInputDto.Name;
                gBrand.Description = BrandInputDto.Description;
                gBrand.ModifiedDate = DateTime.Now;

                _unitOfWork.BrandRepository.Update(gBrand);
                await _unitOfWork.CommitAsync();

                var BrandOutput = _mapper.Map<BrandOutputDto>(gBrand);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = BrandOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrandServices -> UpdateBrand({JsonSerializer.Serialize(BrandInputDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<BrandOutputDto>> DeleteBrand(Guid id)
        {
            try
            {
                var gBrand = await _unitOfWork.BrandRepository.GetAsync(c => c.Id.Equals(id));

                if (gBrand == null)
                {
                    return new ResponseHandle<BrandOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get [Brand] with [id]: {id}"
                    };
                }

                _unitOfWork.BrandRepository.Remove(gBrand);
                await _unitOfWork.CommitAsync();


                var cateOutput = _mapper.Map<BrandOutputDto>(gBrand);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = cateOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrandServices -> DeleteBrand({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
