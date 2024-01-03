using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.OrderServices;
using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Controllers
{
    [Authorize]
    [Route("api/orders")]
    [ApiController]
    public class OrderController : Controller, IOrderServices
    {
        private IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }      

        [HttpGet]
        public ValueTask<ResponseHandle<OrderListOutputDto>> GetAllOrders()
        {
            return _orderServices.GetAllOrders();
        }

        [HttpGet("{id}")]
        public ValueTask<ResponseHandle<OrderOutputDto>> GetOrder(Guid id)
        {
            return _orderServices.GetOrder(id);
        }

        [HttpPost]
        public ValueTask<ResponseHandle<OrderOutputDto>> CreateOrder(OrderInputDto orderCreateDto)
        {
            return _orderServices.CreateOrder(orderCreateDto);
        }
    }
}
