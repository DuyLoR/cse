using System.ComponentModel.DataAnnotations;
namespace CSE_website.Validations;

public class FileExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public FileExtensionsAttribute(string extensions)
    {
        _extensions = extensions.Split(',');
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension))
            {
                return new ValidationResult($"Đuôi file phải thuộc: {string.Join(", ", _extensions)}");
            }
        }
        return ValidationResult.Success;
    }
}
