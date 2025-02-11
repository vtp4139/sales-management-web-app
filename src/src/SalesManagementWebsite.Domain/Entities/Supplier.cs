using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManagementWebsite.Domain.Entities
{
    public class Supplier : BaseModel
    {
        [Required]
        public required string CompanyName { get; set; }

        [Required]
        public required string Address { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public required string Phone { get; set; }

        public string? City { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();
    }
}
