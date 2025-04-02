using SalesManagementWebsite.Client.Paths;
using SalesManagementWebsite.Client.Services.Intefaces;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Client.Services.API
{
    public class BrandService : IBrandService
    {
        private readonly IApiService _apiService;

        public BrandService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<ResponseHandle<BrandOutputDto>> GetAllBrands()
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<BrandOutputDto>>(
                            method: HttpMethod.Get,
                            endpoint: InternalAPIs.GetAllBrands);

            return res;
        }

        public async Task<ResponseHandle<BrandOutputDto>> CreateBrand(BrandCreateDto input)
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<BrandOutputDto>>(
                            method: HttpMethod.Post,
                            endpoint: InternalAPIs.CreateBrand,
                            requestData: input);

            return res;
        }

        public async Task<ResponseHandle<BrandOutputDto>> UpdateBrand(Guid id, BrandInputDto input)
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<BrandOutputDto>>(
                            method: HttpMethod.Put,
                            endpoint: string.Format(InternalAPIs.UpdateBrand, id),
                            requestData: input);

            return res;
        }

        public async Task<ResponseHandle<BrandOutputDto>> DeleteBrand(Guid id)
        {
            var res = await _apiService.SendRequestAsync<ResponseHandle<BrandOutputDto>>(
                            method: HttpMethod.Delete,
                            endpoint: string.Format(InternalAPIs.DeleteBrand, id));

            return res;
        }
    }
}
