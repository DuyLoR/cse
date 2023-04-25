using System.ComponentModel.DataAnnotations;

namespace CSE_website.Validations;

public class FileMaxSizeAttribute : ValidationAttribute
{
    private readonly int _capacity;
    public FileMaxSizeAttribute(int capacity) // MB
    {
        _capacity = capacity;
        ErrorMessage = $"Kích thước file phải nhỏ hơn {capacity} MB";
    }
        
    public override bool IsValid(object? value)
    {
        if (value == null) return true;
            
        IFormFile file = (IFormFile) value;

        return file.Length <= (_capacity * 1024 * 1024);
    }
}