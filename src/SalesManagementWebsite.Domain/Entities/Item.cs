
namespace SalesManagementWebsite.Domain.Entities
{
    public class Item : BaseModel
    {
        public string Name { get; set; } = string.Empty;   
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        //Foreign key for Standard
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        //Foreign key for Standard
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
