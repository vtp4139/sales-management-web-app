using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Contracts.Dtos.Item
{
    public class ItemInputDto
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} is empty!")]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is empty!")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "{0} is empty!")]
        public int Quantity { get; set; }


        [Required(ErrorMessage = "{0} is empty!")]
        public Guid CategoryId { get; set; }


        [Required(ErrorMessage = "{0} is empty!")]
        public Guid BrandId { get; set; }


        [Required(ErrorMessage = "{0} is empty!")]
        public Guid SupplierId { get; set; }
    }
}
