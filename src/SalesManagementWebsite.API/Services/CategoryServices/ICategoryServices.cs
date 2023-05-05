using SalesManagementWebsite.Contracts.Dtos.Category;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.API.Services.CategoryServices
{
    public interface ICategoryServices
    {
        ValueTask<ResponseHandle<CategoryOutputDto>> GetAllCategories();
        ValueTask<ResponseHandle<CategoryOutputDto>> CreateCategory(CategoryCreateDto categoryCreateDto);
        ValueTask<ResponseHandle<CategoryOutputDto>> GetCategory(Guid id);
        ValueTask<ResponseHandle<CategoryOutputDto>> UpdateCategory(CategoryInputDto categoryInputDto);
        ValueTask<ResponseHandle<CategoryOutputDto>> DeleteCategory(Guid id);
    }
}
