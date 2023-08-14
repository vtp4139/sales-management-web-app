using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Order;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Domain.Entities;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace SalesManagementWebsite.Core.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderServices(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderServices> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
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

        public async ValueTask<ResponseHandle<OrderOutputDto>> CreateOrder(OrderInputDto orderCreateDto)
        {
            try
            {
                //(1) Create Order
                var order = _mapper.Map<Order>(orderCreateDto);
                order.UserId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst("user_id")?.Value);
                _unitOfWork.OrderRepository.Add(order);


                //(2) Add roles for user
                foreach (var od in orderCreateDto.OrderDetails)
                {   
                    //(3) Check quantity item > quantity input or not ?
                    var itemCheck = await _unitOfWork.ItemRepository.GetItemById(od.ItemId);

                    if (itemCheck == null)
                    {
                        _logger.LogError($@"OrderServices -> CreateOrder({JsonSerializer.Serialize(orderCreateDto)}) err- Can not get [Item] with [id]: {od.ItemId}");

                        return new ResponseHandle<OrderOutputDto>
                        {
                            IsSuccess = true,
                            StatusCode = (int)HttpStatusCode.NotFound,
                            Data = null,
                            ErrorMessage = $"Can not get [Item] with [id]: {od.ItemId}"
                        };
                    }

                    if (itemCheck.Quantity < od.Quantity)
                    {
                        _logger.LogError($@"OrderServices -> CreateOrder({JsonSerializer.Serialize(orderCreateDto)}) err-[Item] with [id]: {od.ItemId} with [Quantity]: {itemCheck.Quantity} do not have enough quantity ({od.Quantity})");

                        return new ResponseHandle<OrderOutputDto>
                        {
                            IsSuccess = true,
                            StatusCode = (int)HttpStatusCode.NotFound,
                            Data = null,
                            ErrorMessage = $"[Item] with [id]: {od.ItemId} with [Quantity]: {itemCheck.Quantity} do not have enough quantity ({od.Quantity})"
                        };
                    }
                    else
                    {
                        //(4) Update quantity for Item
                        itemCheck.Quantity -= od.Quantity;
                        _unitOfWork.ItemRepository.Update(itemCheck);
                    }

                    //(5) Add OrderDetail
                    _unitOfWork.OrderDetailRepository.Add(new OrderDetail
                    {
                        Quantity = od.Quantity,
                        UnitPrice = od.UnitPrice,
                        Discount = od.Discount,
                        OrderId = order.Id,
                        ItemId = od.ItemId,
                    });
                }

                await _unitOfWork.CommitAsync();

                //Output
                var orderOutput = _mapper.Map<OrderOutputDto>(order);
                orderOutput.OrderDetails = _mapper.Map<List<OrderDetailOutputDto>>(order.OrderDetails);

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
                _logger.LogError($"OrderServices -> CreateOrder({JsonSerializer.Serialize(orderCreateDto)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
