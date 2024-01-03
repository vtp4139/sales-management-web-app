using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Utilities;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BrandServices> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_LIST, nameof(Brand))
                    };
                }

                var brandListOutput = _mapper.Map<List<BrandOutputDto>>(gBrandList);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    ListData = brandListOutput
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
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Brand), id)
                    };
                }

                var brandOutput = _mapper.Map<BrandOutputDto>(gBrand);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = brandOutput
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

                brand.CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;

                _unitOfWork.BrandRepository.Add(brand);
                await _unitOfWork.CommitAsync();

                var brandOutput = _mapper.Map<BrandOutputDto>(brand);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = brandOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrandServices -> CreateBrand({JsonSerializer.Serialize(brandCreateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<BrandOutputDto>> UpdateBrand(Guid id, BrandInputDto BrandInputDto)
        {
            try
            {
                var brand = await _unitOfWork.BrandRepository.GetAsync(c => c.Id.Equals(id));

                if (brand == null)
                {
                    return new ResponseHandle<BrandOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Brand), id)
                    };
                }

                //Mapping field modify
                brand.Name = BrandInputDto.Name;
                brand.Description = BrandInputDto.Description;
                brand.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;
                brand.ModifiedDate = DateTime.Now;

                _unitOfWork.BrandRepository.Update(brand);
                await _unitOfWork.CommitAsync();

                var BrandOutput = _mapper.Map<BrandOutputDto>(brand);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = BrandOutput
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
                var brand = await _unitOfWork.BrandRepository.GetAsync(c => c.Id.Equals(id));

                if (brand == null)
                {
                    return new ResponseHandle<BrandOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Brand), id)
                    };
                }

                _unitOfWork.BrandRepository.Remove(brand);
                await _unitOfWork.CommitAsync();


                var brandOutput = _mapper.Map<BrandOutputDto>(brand);

                return new ResponseHandle<BrandOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = brandOutput
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
