using Microsoft.AspNetCore.Mvc;
using Nest;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.Category;

namespace SalesManagementWebsite.Client.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<ActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();

            return View(categories);
        }

        public async Task<ActionResult> CreateCategory(CategoryCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var categories = await _categoryService.CreateCategory(input);

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<ActionResult> UpdateCategory(Guid id, CategoryInputDto input)
        {
            if (ModelState.IsValid)
            {
                var categories = await _categoryService.UpdateCategory(id, input);

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<ActionResult> DeleteCategory(Guid id)
        {
            if (ModelState.IsValid)
            {
                var categories = await _categoryService.DeleteCategory(id);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
