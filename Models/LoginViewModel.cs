using CSE_website.Constants;
using CSE_website.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_website.Models
{
    public class LoginViewModel
    {
        [Column(TypeName = "nvarchar(255)")]
        [EmailAddress(ErrorMessage = $"Email không hợp lệ!")]
        [Required(ErrorMessage = $"Email và mật khẩu không được để trống!")]
        [UniqueEmail]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = $"Email và mật khẩu không được để trống!")]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "Mật khẩu không hợp lệ!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
