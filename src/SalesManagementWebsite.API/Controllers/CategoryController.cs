using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.CategoryServices;
using SalesManagementWebsite.Contracts.Dtos.Category;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.API.Controllers
{
    [Authorize]
    [Route("api/category")]
    [ApiController] 
    public class CategoryController : Controller, ICategoryServices
    {
        private ICategoryServices _categoryServices { get; set; }

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet("get-all-category")]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> GetAllCategories()
        {
            return await _categoryServices.GetAllCategories();
        }

        [HttpGet("get-category/{id}")]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> GetCategory(Guid id)
        {
            return await _categoryServices.GetCategory(id);
        }

        [HttpPost("create-category")]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            return await _categoryServices.CreateCategory(categoryCreateDto);
        }

        [HttpPut("update-category")]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> UpdateCategory(CategoryInputDto categoryInputDto)
        {
            return await _categoryServices.UpdateCategory(categoryInputDto);
        }

        [HttpDelete("delete-category")]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> DeleteCategory(Guid id)
        {
            return await _categoryServices.DeleteCategory(id);
        }
    }
}
