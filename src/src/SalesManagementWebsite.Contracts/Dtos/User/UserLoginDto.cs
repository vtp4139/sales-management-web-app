using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SalesManagementWebsite.Contracts.Dtos.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public required string Password { get; set; }
    }
}
