using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.CategoryServices;
using SalesManagementWebsite.Contracts.Dtos.Category;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Controllers
{
    //[Authorize]
    [Route("api/categories")]
    [ApiController] 
    public class CategoryController : Controller, ICategoryServices
    {
        private ICategoryServices _categoryServices { get; set; }

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }

        [HttpGet]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> GetAllCategories()
        {
            return await _categoryServices.GetAllCategories();
        }

        [HttpGet("{id}")]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> GetCategory(Guid id)
        {
            return await _categoryServices.GetCategory(id);
        }

        [HttpPost]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            return await _categoryServices.CreateCategory(categoryCreateDto);
        }

        [HttpPut("{id}")]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> UpdateCategory(Guid id, CategoryInputDto categoryInputDto)
        {
            return await _categoryServices.UpdateCategory(id, categoryInputDto);
        }

        [HttpDelete("{id}")]
        public async ValueTask<ResponseHandle<CategoryOutputDto>> DeleteCategory(Guid id)
        {
            return await _categoryServices.DeleteCategory(id);
        }
    }
}
