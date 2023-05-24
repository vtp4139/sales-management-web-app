
using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Contracts.Dtos.Customer
{
    public class CustomerCreateDto
    {
        [Required(ErrorMessage = "{0} is empty!")]
        [StringLength(50)]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is empty!")]
        [StringLength(200)]
        public string Address { get; set; } = string.Empty;

        [StringLength(20)]
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;

        [Phone]
        public string Phone { get; set; } = string.Empty;

        public string Fax { get; set; } = string.Empty;
    }
}
