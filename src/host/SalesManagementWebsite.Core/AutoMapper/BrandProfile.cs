using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Core.AutoMapper
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
