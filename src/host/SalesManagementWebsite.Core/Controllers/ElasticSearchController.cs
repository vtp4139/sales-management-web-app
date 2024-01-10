using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesManagementWebsite.Contracts.Dtos.ElasticSearch;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;
using SalesManagementWebsite.Core.Services.ElasticSearchServices;
using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Core.Controllers
{
    //[Authorize]
    [Route("api/elastic-search")]
    [ApiController]
    public class ElasticSearchController : Controller, IElasticSearchServices
    {
        private IElasticSearchServices _elasticSearchServices { get; set; }

        public ElasticSearchController(IElasticSearchServices elasticSearchServices)
        {
            _elasticSearchServices = elasticSearchServices;
        }

        [HttpPost("sync-items")]
        public async ValueTask<bool> SyncItemToES(ItemIndex itemIndex)
        {
            return await _elasticSearchServices.SyncItemToES(itemIndex);
        }

        [HttpGet("items/{id}")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> SearchItemOnES(string id)
        {
            return await _elasticSearchServices.SearchItemOnES(id);
        }

        [HttpGet("items")]
        public async ValueTask<ResponseHandle<ItemOutputDto>> SearchItemsOnES([FromQuery] List<string> ids)
        {
            return await _elasticSearchServices.SearchItemsOnES(ids);
        }
    }
}
