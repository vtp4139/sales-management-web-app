using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Services.BrandServices
{
    public interface IBrandServices
    {
        ValueTask<ResponseHandle<BrandOutputDto>> GetAllBrands();
        ValueTask<ResponseHandle<BrandOutputDto>> CreateBrand(BrandCreateDto categoryCreateDto);
        ValueTask<ResponseHandle<BrandOutputDto>> GetBrand(Guid id);
        ValueTask<ResponseHandle<BrandOutputDto>> UpdateBrand(Guid id, BrandInputDto categoryInputDto);
        ValueTask<ResponseHandle<BrandOutputDto>> DeleteBrand(Guid id);
    }
}
