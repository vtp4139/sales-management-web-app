using SalesManagementWebsite.Contracts.Dtos.Item;
using SalesManagementWebsite.Contracts.Dtos.Response;

namespace SalesManagementWebsite.Core.Services.ItemServices
{
    public interface IItemServices
    {
        ValueTask<ResponseHandle<ItemListDto>> GetAllItems();
        ValueTask<ResponseHandle<ItemOutputDto>> CreateItem(ItemCreateDto itemCreateDto);
        ValueTask<ResponseHandle<ItemOutputDto>> GetItemById(Guid id);
        ValueTask<ResponseHandle<ItemOutputDto>> UpdateItem(Guid id, ItemInputDto itemInputDto);
        ValueTask<ResponseHandle<ItemOutputDto>> DeleteItem(Guid id);
    }
}
