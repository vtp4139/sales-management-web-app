using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementWebsite.Domain.Entities
{
    public class Order : BaseModel
    {
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        //Get employee create order
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public required User User { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }
        public required Customer Customer { get; set; }
    }
}
