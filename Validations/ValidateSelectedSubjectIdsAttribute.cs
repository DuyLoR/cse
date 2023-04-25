using System.ComponentModel.DataAnnotations;
using CSE_website.Data;
using CSE_website.Models;

namespace CSE_website.Validations;

public class ValidateSelectedSubjectIdsAttribute : ValidationAttribute
{
    public ValidateSelectedSubjectIdsAttribute()
    {
        ErrorMessage = "Có lỗi đã xảy ra, vui lòng kiểm tra lại thông tin!";
    }
        
    protected override ValidationResult IsValid(object? selIds, ValidationContext validationContext)
    {
        if(selIds is null) return ValidationResult.Success;
        if(selIds is not List<int> subjectIds) return new ValidationResult(ErrorMessage);

        // Lấy thông tin đối tượng Model
        var model = (Lecturer)validationContext.ObjectInstance;

        var dbContext = (CSEWebsiteContext)validationContext.GetService(typeof(CSEWebsiteContext));

        var isExist = dbContext.Departments
            .Where(depart => depart.Id == model.SelectedDepartmentId)
            .SelectMany(depart => depart.Subjects.Select(subject => subject.Id))
            .Any(subjectId => subjectIds.Contains(subjectId));

        if(isExist == false) return new ValidationResult(ErrorMessage);
        return ValidationResult.Success;
    }
}