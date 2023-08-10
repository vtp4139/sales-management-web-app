using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Services.ItemServices
{
    public interface IItemServices
    {
        ValueTask<ResponseHandle<ItemOutputDto>> GetAllItems();
        ValueTask<ResponseHandle<ItemOutputDto>> CreateItem(ItemCreateDto itemCreateDto);
        ValueTask<ResponseHandle<ItemOutputDto>> GetItem(Guid id);
        ValueTask<ResponseHandle<ItemOutputDto>> UpdateItem(ItemInputDto itemInputDto);
        ValueTask<ResponseHandle<ItemOutputDto>> DeleteItem(Guid id);
    }
}
