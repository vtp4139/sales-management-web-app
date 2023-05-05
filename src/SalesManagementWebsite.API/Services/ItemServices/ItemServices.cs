using AutoMapper;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Domain.UnitOfWork;
using System.Net;
using System.Text.Json;

namespace SalesManagementWebsite.API.Services.ItemServices
{
    public class ItemServices : IItemServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ItemServices(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration, ILogger<ItemServices> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async ValueTask<ResponseHandle<ItemOutputDto>> GetItem(Guid id)
        {
            try
            {
                var gItem = await _unitOfWork.ItemRepository.GetItem(id);

                if (gItem == null)
                {
                    return new ResponseHandle<ItemOutputDto>
                    {
                        IsSuccess = false,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        Data = null,
                        ErrorMessage = $"Can not get item with id: {id}"
                    };
                }

                var itemOutput = _mapper.Map<ItemOutputDto>(gItem);

                return new ResponseHandle<ItemOutputDto>
                {
                    IsSuccess = true,
                    StatusCode = (int)HttpStatusCode.OK,
                    Data = itemOutput,
                    ErrorMessage = string.Empty
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"ItemService -> GetItem({JsonSerializer.Serialize(id)}) " +
                                 $"- Have exception: {ex}, at {DateTime.UtcNow.ToLongTimeString()}");
                throw;
            }
        }
    }
}
