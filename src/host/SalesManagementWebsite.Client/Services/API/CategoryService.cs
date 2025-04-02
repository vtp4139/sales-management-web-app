using SalesManagementWebsite.Client.Paths;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.Category;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Client.Services.API
{
    public class CategoryService : ICategoryService
    {
        private readonly IApiService _apiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(IApiService apiService, IHttpContextAccessor httpContextAccessor)
        {
            _apiService = apiService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseHandle<CategoryOutputDto>> GetAllCategories()
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<CategoryOutputDto>>(
                            method: HttpMethod.Get, 
                            endpoint: InternalAPIs.GetAllCategories);

            return res;
        }

        public async Task<ResponseHandle<CategoryOutputDto>> CreateCategory(CategoryCreateDto input)
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<CategoryOutputDto>>(
                            method: HttpMethod.Post,
                            endpoint: InternalAPIs.CreateCategory,
                            requestData: input);

            return res;
        }

        public async Task<ResponseHandle<CategoryOutputDto>> UpdateCategory(Guid id, CategoryInputDto input)
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<CategoryOutputDto>>(
                            method: HttpMethod.Put,
                            endpoint: string.Format(InternalAPIs.UpdateCategory, id),
                            requestData: input);

            return res;
        }

        public async Task<ResponseHandle<CategoryOutputDto>> DeleteCategory(Guid id)
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<CategoryOutputDto>>(
                            method: HttpMethod.Delete,
                            endpoint: string.Format(InternalAPIs.DeleteCategory, id));

            return res;
        }
    }
}
