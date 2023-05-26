using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.API.AutoMapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderListOutputDto>();
            CreateMap<Order, OrderOutputDto>();
            CreateMap<OrderDetail, OrderDetailOutputDto>();
        }
    }
}
