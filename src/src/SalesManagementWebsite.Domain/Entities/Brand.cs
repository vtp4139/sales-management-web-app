using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Domain.Entities
{
    public class Brand : BaseModel
    {
        [Required]
        [Column(TypeName = "NVARCHAR(500)")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public List<Item> Items { get; set; } = new List<Item>();
    }
}
