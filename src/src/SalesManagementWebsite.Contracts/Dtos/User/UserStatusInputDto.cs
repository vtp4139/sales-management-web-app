using System.ComponentModel.DataAnnotations;

namespace SalesManagementWebsite.Contracts.Dtos.User
{
    public class UserStatusInputDto
    {
        [Required(ErrorMessage = "{0} is empty!")]
        public string UserName { get; set; } = string.Empty;

        public int StatusUser { get; set; }
    }
}
