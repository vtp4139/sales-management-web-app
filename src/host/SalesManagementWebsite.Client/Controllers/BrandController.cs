using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.Brand;

namespace SalesManagementWebsite.Client.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        public async Task<ActionResult> Index()
        {
            var brands = await _brandService.GetAllBrands();

            return View(brands);
        }

        public async Task<ActionResult> CreateBrand(BrandCreateDto input)
        {
            if (ModelState.IsValid)
            {
                var brands = await _brandService.CreateBrand(input);

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<ActionResult> UpdateBrand(Guid id, BrandInputDto input)
        {
            if (ModelState.IsValid)
            {
                var brands = await _brandService.UpdateBrand(id, input);

                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<ActionResult> DeleteBrand(Guid id)
        {
            if (ModelState.IsValid)
            {
                var brands = await _brandService.DeleteBrand(id);

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}
