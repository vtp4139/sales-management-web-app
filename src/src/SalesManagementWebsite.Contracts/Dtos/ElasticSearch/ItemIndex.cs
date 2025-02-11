using Nest;
using SalesManagementWebsite.Contracts.Dtos.Item;

namespace SalesManagementWebsite.Contracts.Dtos.ElasticSearch
{
    public class ItemIndex : ItemInputDto
    {
        [Keyword]
        public string Id { get; set; }
    }
}
