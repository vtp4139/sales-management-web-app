using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Category;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Contracts.Utilities;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.Core.Services.CategoryServices
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CategoryServices> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async ValueTask<ResponseHandle<CategoryOutputDto>> GetAllCategories()
        {
            try
            {
                var gCategoryList = await _unitOfWork.CategoryRepository.GetAllAsync();

                if (gCategoryList == null)
                {
                    return new ResponseHandle<CategoryOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_LIST, nameof(Category))
                    };
                }

                var cateListOutput = _mapper.Map<List<CategoryOutputDto>>(gCategoryList);

                return new ResponseHandle<CategoryOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    ListData = cateListOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoryServices -> GetAllCategories() " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<CategoryOutputDto>> GetCategory(Guid id)
        {
            try
            {
                var gCategory = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id.Equals(id));

                if (gCategory == null)
                {
                    return new ResponseHandle<CategoryOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Category), id)
                    };
                }

                var cateOutput = _mapper.Map<CategoryOutputDto>(gCategory);

                return new ResponseHandle<CategoryOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = cateOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoryServices -> GetCategory({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<CategoryOutputDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            try
            {
                var category = _mapper.Map<Category>(categoryCreateDto);

                category.CreatedBy = _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;

                _unitOfWork.CategoryRepository.Add(category);
                await _unitOfWork.CommitAsync();

                var categoryOutput = _mapper.Map<CategoryOutputDto>(category);

                return new ResponseHandle<CategoryOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = categoryOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoryServices -> CreateCategory({JsonSerializer.Serialize(categoryCreateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<CategoryOutputDto>> UpdateCategory(Guid id, CategoryInputDto categoryInputDto)
        {
            try
            {
                var gCategory = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id.Equals(id));

                if (gCategory == null)
                {
                    return new ResponseHandle<CategoryOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Category), id)
                    };
                }

                //Mapping field modify
                gCategory.Name = categoryInputDto.Name;
                gCategory.Description = categoryInputDto.Description;
                gCategory.ModifiedBy = _httpContextAccessor.HttpContext?.User.FindFirst("username")?.Value;
                gCategory.ModifiedDate = DateTime.Now;

                _unitOfWork.CategoryRepository.Update(gCategory);
                await _unitOfWork.CommitAsync();

                var categoryOutput = _mapper.Map<CategoryOutputDto>(gCategory);

                return new ResponseHandle<CategoryOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = categoryOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoryServices -> UpdateCategory({JsonSerializer.Serialize(categoryInputDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<CategoryOutputDto>> DeleteCategory(Guid id)
        {
            try
            {
                var gCategory = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id.Equals(id));

                if (gCategory == null)
                {
                    return new ResponseHandle<CategoryOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = string.Format(MessageHandle.ERROR_NOT_FOUND_BY_ID, nameof(Category), id)
                    };
                }

                _unitOfWork.CategoryRepository.Remove(gCategory);
                await _unitOfWork.CommitAsync();


                var cateOutput = _mapper.Map<CategoryOutputDto>(gCategory);

                return new ResponseHandle<CategoryOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = cateOutput
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"CategoryServices -> DeleteCategory({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
