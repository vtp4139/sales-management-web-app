using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.API.AutoMapper
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<BrandInputDto, Brand>();
            CreateMap<BrandCreateDto, Brand>();
            CreateMap<Brand, BrandOutputDto>();
        }
    }
}
