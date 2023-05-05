using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.BrandServices;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.API.Controllers
{
    //[Authorize]
    [Route("api/brand")]
    [ApiController]
    public class BrandController : Controller, IBrandServices
    {
        private IBrandServices _brandServices { get; set; }

        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        [HttpGet("get-all-brand")]
        public async ValueTask<ResponseHandle<BrandOutputDto>> GetAllBrands()
        {
            return await _brandServices.GetAllBrands();
        }

        [HttpGet("get-brand")]
        public async ValueTask<ResponseHandle<BrandOutputDto>> GetBrand(Guid id)
        {
            return await _brandServices.GetBrand(id);
        }

        [HttpPost("create-brand")]
        public async ValueTask<ResponseHandle<BrandOutputDto>> CreateBrand(BrandCreateDto brandCreateDto)
        {
            return await _brandServices.CreateBrand(brandCreateDto);
        }

        [HttpPut("update-brand")]
        public async ValueTask<ResponseHandle<BrandOutputDto>> UpdateBrand(BrandInputDto brandInputDto)
        {
            return await _brandServices.UpdateBrand(brandInputDto);
        }

        [HttpDelete("delete-brand")]
        public async ValueTask<ResponseHandle<BrandOutputDto>> DeleteBrand(Guid id)
        {
            return await _brandServices.DeleteBrand(id);
        }
    }
}
