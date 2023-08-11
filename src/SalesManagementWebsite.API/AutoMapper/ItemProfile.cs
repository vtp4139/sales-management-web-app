using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Core.AutoMapper
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemInputDto, Item>();
            CreateMap<ItemCreateDto, Item>();
            CreateMap<Item, ItemOutputDto>()
               .ForMember(dest => dest.Category, opt => opt.MapFrom(x => x.Category))
               .ForMember(dest => dest.Brand, opt => opt.MapFrom(x => x.Brand))
               .ForMember(dest => dest.Supplier, opt => opt.MapFrom(x => x.Supplier));
            CreateMap<Item, ItemListDto>();
        }
    }
}
