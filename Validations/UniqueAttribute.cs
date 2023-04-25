using System.ComponentModel.DataAnnotations;
using System.Linq;
using CSE_website.Data;
using CSE_website.Models;

namespace CSE_website.Validations;

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value == null || value == "") return ValidationResult.Success;

        var dbContext = (CSEWebsiteContext)validationContext.GetService(typeof(CSEWebsiteContext));
        var propertyName = validationContext.MemberName;
        Type entityType = validationContext.ObjectInstance.GetType();
        bool? duplicate = false;
        
        if (entityType == typeof(Lecturer))
        {
            var entity = (Lecturer)validationContext.ObjectInstance;
            duplicate = dbContext.Lecturers
                .Any(l => l.Id != entity.Id && l.Email == (string)value);
        }
        else if (entityType == typeof(FacultyOffice))
        {
            var entity = (FacultyOffice)validationContext.ObjectInstance;
            duplicate = dbContext.FacultyOffices
                .Any(l => l.Id != entity.Id && l.Email == (string)value);
        }
        else if (entityType == typeof(Partner))
        {
            var entity = (Partner)validationContext.ObjectInstance;
            duplicate = dbContext.Partners
                .Any(l => l.Id != entity.Id && l.Email == (string)value);
        }

        if (duplicate is true)
        {
            return new ValidationResult($"{propertyName} đã được sử dụng.");
        }

        return ValidationResult.Success;
    }
}

public class UniquePhoneAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value == null || value == "") return ValidationResult.Success;

        var dbContext = (CSEWebsiteContext)validationContext.GetService(typeof(CSEWebsiteContext));
        var propertyName = validationContext.MemberName;
        Type entityType = validationContext.ObjectInstance.GetType();
        bool? duplicate = false;

        if (entityType == typeof(Lecturer))
        {
            var entity = (Lecturer)validationContext.ObjectInstance;
            duplicate = dbContext.Lecturers
                .Any(l => l.Id != entity.Id && l.Phone == (string)value);
        }
        else if (entityType == typeof(FacultyOffice))
        {
            var entity = (FacultyOffice)validationContext.ObjectInstance;
            duplicate = dbContext.FacultyOffices
                .Any(l => l.Id != entity.Id && l.Phone == (string)value);
        }
        else if (entityType == typeof(Partner))
        {
            var entity = (Partner)validationContext.ObjectInstance;
            duplicate = dbContext.Partners
                .Any(l => l.Id != entity.Id && l.Phone == (string)value);
        }

        if (duplicate is true)
        {
            return new ValidationResult($"{propertyName} đã được sử dụng.");
        }

        return ValidationResult.Success;
    }
}

public class UniqueLinkAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value == null || value == "") return ValidationResult.Success;
        
        var dbContext = (CSEWebsiteContext)validationContext.GetService(typeof(CSEWebsiteContext));
        var propertyName = validationContext.MemberName;
        Type entityType = validationContext.ObjectInstance.GetType();
        bool? duplicate = false;

        if (entityType == typeof(Lecturer))
        {
            var entity = (Lecturer)validationContext.ObjectInstance;
            duplicate = dbContext.Lecturers
                .Any(l => l.Id != entity.Id && l.LinkBlog == (string)value);
        }
        else if (entityType == typeof(Partner))
        {
            var entity = (Partner)validationContext.ObjectInstance;
            duplicate = dbContext.Partners
                .Any(l => l.Id != entity.Id && l.Link == (string)value);
        }

        if (duplicate is true)
        {
            return new ValidationResult($"{propertyName} đã được sử dụng.");
        }

        return ValidationResult.Success;
    }
}

public class UniqueCodeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if(value == null || value == "") return ValidationResult.Success;
        
        var dbContext = (CSEWebsiteContext)validationContext.GetService(typeof(CSEWebsiteContext));
        var propertyName = validationContext.MemberName;
        Type entityType = validationContext.ObjectInstance.GetType();
        bool? duplicate = false;

        if (entityType == typeof(Lecturer))
        {
            var entity = (Lecturer)validationContext.ObjectInstance;
            duplicate = dbContext.Lecturers
                .Any(l => l.Id != entity.Id && l.LecturerCode == (string)value);
        }

        if (duplicate is true)
        {
            return new ValidationResult(ErrorMessage);
        }

        return ValidationResult.Success;
    }
}