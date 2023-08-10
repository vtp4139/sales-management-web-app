﻿using AutoMapper;
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
            CreateMap<Item, ItemOutputDto>();
        }
    }
}
