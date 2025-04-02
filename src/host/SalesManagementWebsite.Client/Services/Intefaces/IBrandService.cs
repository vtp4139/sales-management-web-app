using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Client.Services.Intefaces
{
    public interface IBrandService
    {
        Task<ResponseHandle<BrandOutputDto>> GetAllBrands();
        Task<ResponseHandle<BrandOutputDto>> CreateBrand(BrandCreateDto input);
        Task<ResponseHandle<BrandOutputDto>> UpdateBrand(Guid id, BrandInputDto input);
        Task<ResponseHandle<BrandOutputDto>> DeleteBrand(Guid id);
    }
}
