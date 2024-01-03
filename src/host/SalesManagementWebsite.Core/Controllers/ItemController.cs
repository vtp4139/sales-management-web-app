using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Core.Services.ItemServices;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Controllers
{
    [Authorize]
    [Route("api/items")]
    [ApiController] 
    public class ItemController : Controller, IItemServices
    {
        private IItemServices _itemServices { get; set; }

        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        [HttpGet]
        public async ValueTask<ResponseHandle<ItemListDto>> GetAllItems()
        {
            return await _itemServices.GetAllItems();
        }

        [HttpGet("{id}")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> GetItemById(Guid id)
        {
            return await _itemServices.GetItemById(id);
        }

        [HttpPost]
        public async ValueTask<ResponseHandle<ItemOutputDto>> CreateItem(ItemCreateDto itemCreateDto)
        {
            return await _itemServices.CreateItem(itemCreateDto);
        }

        [HttpPut("{id}")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> UpdateItem(Guid id, ItemInputDto itemInputDto)
        {
            return await _itemServices.UpdateItem(id, itemInputDto);
        }

        [HttpDelete("{id}")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> DeleteItem(Guid id)
        {
            return await _itemServices.DeleteItem(id);
        }

    }
}
