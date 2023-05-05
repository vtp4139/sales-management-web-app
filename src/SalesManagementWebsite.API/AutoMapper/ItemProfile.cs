using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.API.AutoMapper
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<ItemInputDto, Item>();
            CreateMap<Item, ItemOutputDto>();
        }
    }
}
