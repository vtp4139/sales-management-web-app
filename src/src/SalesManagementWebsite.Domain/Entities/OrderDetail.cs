using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementWebsite.Domain.Entities
{
    public class OrderDetail : BaseModel
    {
        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required decimal UnitPrice { get; set; }

        public decimal? Discount { get; set; }

        [ForeignKey("Order")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Item")]
        public Guid ItemId { get; set; }
        public Item Item { get; set; }
    }
}
