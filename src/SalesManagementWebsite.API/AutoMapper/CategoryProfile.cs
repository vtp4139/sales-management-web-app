using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Category;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.API.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryInputDto, Category>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<Category, CategoryOutputDto>();
        }
    }
}
