
namespace SalesManagementWebsite.Domain.Entities
{
    public class Item : BaseModel
    {
        public string Name { get; set; } = string.Empty;   
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        //Foreign key for Standard
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //Foreign key for Standard
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
