
using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Contracts.Dtos.Brand
{
    public class BrandCreateDto
    {
        [Required(ErrorMessage = "{0} is empty!")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is empty!")]
        [StringLength(300)]
        public string Description { get; set; } = string.Empty;
    }
}
