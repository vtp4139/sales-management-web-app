using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Contracts.Dtos.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "{0} is empty!")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} is empty!")]
        public string Password { get; set; } = string.Empty;
    }
}
