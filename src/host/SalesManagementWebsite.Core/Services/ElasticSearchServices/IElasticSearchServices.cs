using SalesManagementWebsite.Contracts.Dtos.ElasticSearch;
using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Services.ElasticSearchServices
{
    public interface IElasticSearchServices
    {
        ValueTask<bool> SyncItemToES(ItemIndex itemIndex);
        ValueTask<ResponseHandle<ItemOutputDto>> SearchItemOnES(string id);
        ValueTask<ResponseHandle<ItemOutputDto>> SearchItemsOnES(List<string> ids);
        ValueTask<bool> DeleteItemOnES(string id);
    }
}
