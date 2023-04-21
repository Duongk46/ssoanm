using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Controllers
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }

    }
}