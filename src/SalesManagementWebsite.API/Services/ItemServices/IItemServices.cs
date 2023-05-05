using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.API.Services.ItemServices
{
    public interface IItemServices
    {
        public ValueTask<ResponseHandle<ItemOutputDto>> GetItem(Guid id);
    }
}
