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
            CreateMap<OrderInputDto, Order>().ForMember(x => x.OrderDetails, opt => opt.Ignore());
            CreateMap<OrderDetailInputDto, OrderDetail>();         
        }
    }
}
