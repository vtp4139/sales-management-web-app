using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Brand;
using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.API.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public OrderServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async ValueTask<ResponseHandle<OrderListOutputDto>> GetAllOrders()
        {
            try
            {
                var gOrderList = await _unitOfWork.OrderRepository.GetAllAsync();

                if (gOrderList == null)
                {
                    return new ResponseHandle<OrderListOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get list [Orders]"
                    };
                }

                var orderListOutput = _mapper.Map<List<OrderListOutputDto>>(gOrderList);

                return new ResponseHandle<OrderListOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = null,
                    ListData = orderListOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrderServices -> GetAllOrders() " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }

        public async ValueTask<ResponseHandle<OrderOutputDto>> GetOrder(Guid id)
        {
            try
            {
                var gOrder = await _unitOfWork.OrderRepository.GetOrderAsync(id);

                if (gOrder == null)
                {
                    return new ResponseHandle<OrderOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get [Order] with [id]: {id}"
                    };
                }

                var orderOutput = _mapper.Map<OrderOutputDto>(gOrder);
                orderOutput.OrderDetails = _mapper.Map<List<OrderDetailOutputDto>>(gOrder.OrderDetails);

                return new ResponseHandle<OrderOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = orderOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrderServices -> GetOrder({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
