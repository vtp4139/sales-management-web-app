using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.ItemServices;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Controllers
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

        [HttpGet("get-all-items")]
        public async ValueTask<ResponseHandle<ItemListDto>> GetAllItems()
        {
            return await _itemServices.GetAllItems();
        }

        [HttpGet("get-item-by-id/{id}")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> GetItemById(Guid id)
        {
            return await _itemServices.GetItemById(id);
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
