using CSE_website.Constants;
using CSE_website.Validations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_website.Models
{
    public class AccountViewModel
    {

        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [EmailAddress(ErrorMessage = $"Địa chỉ email {ValidationErrorsConstants.INVALID}")]
        [Required(ErrorMessage = $"Địa chỉ email {ValidationErrorsConstants.REQUIRED}")]
        [UniqueEmail]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = $"Mật khẩu {ValidationErrorsConstants.REQUIRED}")]
        [RegularExpression(@"^.{8,}$", ErrorMessage = "Mật khẩu phải có ít nhất 8 kí tự.")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Mật khẩu mới không khớp.")]
        [Required(ErrorMessage = $"Xác nhận mật khẩu {ValidationErrorsConstants.REQUIRED}")]
        public string ConfirmPassword { get; set; }
        public bool ChangePassword { get; set; }
        public bool Status { get; set; }
        public int? PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
