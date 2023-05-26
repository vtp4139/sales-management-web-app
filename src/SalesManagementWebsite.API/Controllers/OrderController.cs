using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.OrderServices;
using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.API.Controllers
{
    //[Authorize]
    [Route("api/order")]
    [ApiController]
    public class OrderController : Controller, IOrderServices
    {
        private IOrderServices _orderServices;

        public OrderController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }

        [HttpGet("get-all-orders")]
        public ValueTask<ResponseHandle<OrderListOutputDto>> GetAllOrders()
        {
            return _orderServices.GetAllOrders();
        }

        [HttpGet("get-order/{id}")]
        public ValueTask<ResponseHandle<OrderOutputDto>> GetOrder(Guid id)
        {
            return _orderServices.GetOrder(id);
        }
    }
}
