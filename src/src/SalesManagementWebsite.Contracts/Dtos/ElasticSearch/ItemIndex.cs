using SalesManagementWebsite.Contracts.Dtos.Item;

namespace SalesManagementWebsite.Contracts.Dtos.ElasticSearch
{
    public class ItemIndex : ItemInputDto
    {
        public string Id { get; set; }
    }
}
