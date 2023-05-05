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
        public IItemServices _itemServices { get; set; }

        public ItemController(IItemServices itemServices)
        {
            _itemServices = itemServices;
        }

        [HttpGet("get-item")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> GetItem(Guid id)
        {
            return await _itemServices.GetItem(id);
        }
    }
}
