using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Domain.Entities;

namespace SalesManagementWebsite.Core.AutoMapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderListOutputDto>();

            CreateMap<User, UserOrderDto>();

            CreateMap<Order, OrderOutputDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(x => x.User))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(x => x.Customer));

            CreateMap<OrderDetail, OrderDetailOutputDto>()
                 .ForMember(dest => dest.ItemName, opt => opt.MapFrom(x => x.Item.Name));

            CreateMap<OrderInputDto, Order>().ForMember(x => x.OrderDetails, opt => opt.Ignore());
            CreateMap<OrderDetailInputDto, OrderDetail>();         
        }
    }
}
