using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementWebsite.Domain.Entities
{
    public class Item : BaseModel
    {
        public string Name { get; set; } = string.Empty;   
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
