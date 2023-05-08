using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.API.Services.ItemServices;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.API.Controllers
{
    [Authorize]
    [Route("api/item")]
    [ApiController] 
    public class ItemController : Controller, IItemServices
    {
        private IItemServices _itemServices { get; set; }

        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        [HttpGet("get-all-item")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> GetAllItems()
        {
            return await _itemServices.GetAllItems();
        }

        [HttpGet("get-item/{id}")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> GetItem(Guid id)
        {
            return await _itemServices.GetItem(id);
        }

        [HttpPost("create-item")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> CreateItem(ItemCreateDto itemCreateDto)
        {
            return await _itemServices.CreateItem(itemCreateDto);
        }

        [HttpPut("update-item")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> UpdateItem(ItemInputDto itemInputDto)
        {
            return await _itemServices.UpdateItem(itemInputDto);
        }

        [HttpDelete("delete-item")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> DeleteItem(Guid id)
        {
            return await _itemServices.DeleteItem(id);
        }

    }
}
