using SalesManagementWebsite.Contracts.Dtos.Category;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Client.Services.Intefaces
{
    public interface ICategoryService
    {
        Task<ResponseHandle<CategoryOutputDto>> GetAllCategories();
        Task<ResponseHandle<CategoryOutputDto>> CreateCategory(CategoryCreateDto input);
        Task<ResponseHandle<CategoryOutputDto>> UpdateCategory(Guid id, CategoryInputDto input);
        Task<ResponseHandle<CategoryOutputDto>> DeleteCategory(Guid id);
    }
}
