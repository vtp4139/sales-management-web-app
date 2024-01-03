using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.BrandServices;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Controllers
{
    [Authorize]
    [Route("api/brands")]
    [ApiController]
    public class BrandController : Controller, IBrandServices
    {
        private IBrandServices _brandServices { get; set; }

        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        [HttpGet]
        public async ValueTask<ResponseHandle<BrandOutputDto>> GetAllBrands()
        {
            return await _brandServices.GetAllBrands();
        }

        [HttpGet("{id}")]
        public async ValueTask<ResponseHandle<BrandOutputDto>> GetBrand(Guid id)
        {
            return await _brandServices.GetBrand(id);
        }

        [HttpPost]
        public async ValueTask<ResponseHandle<BrandOutputDto>> CreateBrand(BrandCreateDto brandCreateDto)
        {
            return await _brandServices.CreateBrand(brandCreateDto);
        }

        [HttpPut("{id}")]
        public async ValueTask<ResponseHandle<BrandOutputDto>> UpdateBrand(Guid id, BrandInputDto brandInputDto)
        {
            return await _brandServices.UpdateBrand(id, brandInputDto);
        }

        [HttpDelete("{id}")]
        public async ValueTask<ResponseHandle<BrandOutputDto>> DeleteBrand(Guid id)
        {
            return await _brandServices.DeleteBrand(id);
        }
    }
}
