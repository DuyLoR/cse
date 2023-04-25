using System.ComponentModel.DataAnnotations;

namespace CSE_website.Validations;

public class NotNullAttribute : ValidationAttribute
{
    public NotNullAttribute()
    {
        ErrorMessage = "Dữ liệu nhập vào lỗi vui lòng kiểm tra lại";
    }
        
    public override bool IsValid(object? value)
    {
        return value != null;
    }
}