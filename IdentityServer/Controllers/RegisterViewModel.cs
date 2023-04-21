using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Controllers
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tài khoản")]
        [Display(Name = "Tài khoản")]
        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        public string Password { get; set; }
        [Display(Name = "Nhập lại mật khẩu")]
        [Compare("Password",ErrorMessage = "Mật khẩu không chính xác")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string Name { get; set; }
        [Display(Name = "Họ và Tên")]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }
        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập Địa chỉ")]
        public string Address { get; set; }
        [Display(Name = "Giới tính")]
        public int Gender { get; set; }
        public string? ReturnUrl { get; set; }
    }
}