using System.ComponentModel.DataAnnotations;
using CSE_website.Data;

namespace CSE_website.Validations;

public class ValidateSelectedIdsAttribute : ValidationAttribute
{
    // Belong to model
    private readonly dynamic _modelName;

    public ValidateSelectedIdsAttribute(string modelName)
    {
        _modelName = modelName;
        ErrorMessage = "Có lỗi đã xảy ra, vui lòng kiểm tra lại thông tin!";
    }
        
    protected override ValidationResult IsValid(object? selIds, ValidationContext validationContext)
    {
        var dbContext = (CSEWebsiteContext)validationContext.GetService(typeof(CSEWebsiteContext));

        bool isExists = false;
        
        if (selIds != null)
        {
            List<int>? selectedIds = null;
            int? selectedId = null;

            if (selIds is int) selectedId = (int)selIds;
            else
            {
                selectedIds = (List<int>)selIds;
                if (selectedIds.Count == 0) return ValidationResult.Success;
            }
            
            switch (_modelName)
            {
                case "Account":
                    isExists = dbContext.Accounts.Any(
                        account => account.Id == selectedId || (selectedIds != null && selectedIds.Contains(account.Id)));
                    break;
                case "Category":
                    isExists = dbContext.Categories.Any(cate => cate.Id == selectedId || (selectedIds != null && selectedIds.Contains(cate.Id)));
                    break;
                case "Department":
                    isExists = dbContext.Departments.Any(dep => dep.Id == selectedId || (selectedIds != null && selectedIds.Contains(dep.Id)));
                    break;
                case "FacultyOffice":
                    isExists = dbContext.FacultyOffices.Any(fa => fa.Id == selectedId || (selectedIds != null && selectedIds.Contains(fa.Id)));
                    break;
                case "Lecturer":
                    isExists = dbContext.Lecturers.Any(lec => lec.Id == selectedId || (selectedIds != null && selectedIds.Contains(lec.Id)));
                    break;
                case "Partner":
                    isExists = dbContext.Partners.Any(par => par.Id == selectedId || (selectedIds != null && selectedIds.Contains(par.Id)));
                    break;
                case "Post":
                    isExists = dbContext.Posts.Any(post => post.Id == selectedId || (selectedIds != null && selectedIds.Contains(post.Id)));
                    break;
                case "Subject":
                    isExists = dbContext.Subjects.Any(subject => subject.Id == selectedId || (selectedIds != null && selectedIds.Contains(subject.Id)));
                    break;
            }
        }

        if (isExists || selIds == null) return ValidationResult.Success;
        return new ValidationResult(ErrorMessage);
    }
}