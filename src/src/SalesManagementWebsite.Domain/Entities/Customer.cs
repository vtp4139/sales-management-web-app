using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Domain.Entities
{
    public class Customer : BaseModel
    {
        [Required]
        [Column(TypeName = "NVARCHAR(500)")]
        public required string CustomerName { get; set; }

        [Column(TypeName = "NVARCHAR(500)")]
        public string? Address { get; set; }

        public string? City { get; set; }

        public string? PostalCode { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string? Phone { get; set; }

        [Column(TypeName = "VARCHAR(20)")]
        public string? Fax { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
