using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Contracts.Dtos.User
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "{0} is empty!")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is empty!")]
        public string Password { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
