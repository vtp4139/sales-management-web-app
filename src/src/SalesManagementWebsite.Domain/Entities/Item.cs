using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementWebsite.Domain.Entities
{
    public class Item : BaseModel
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public required Brand Brand { get; set; }

        [ForeignKey("Supplier")]
        public Guid SupplierId { get; set; }
        public required Supplier Supplier { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
