using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSE_website.Constants;
using CSE_website.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSE_website.Models
{
    [BindProperties]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]
    [Index(nameof(LinkBlog), IsUnique = true)]
    [Index(nameof(LecturerCode), IsUnique = true)]
    public class Lecturer
    {
        public Lecturer()
        {
            this.Subjects = new List<Subject>();
        }

        #region Field
        public int Id { get; set; }

        [Required(ErrorMessage = $"Mã giảng viên {ValidationErrorsConstants.REQUIRED}")]
        [UniqueCode(ErrorMessage = "Mã giảng viên đã được sử dụng")]
        [Column(TypeName = "nvarchar(255)")]
        public string LecturerCode { get; set; }

        [Required(ErrorMessage = $"Tên {ValidationErrorsConstants.REQUIRED}")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }
        
        [Column(TypeName = "nvarchar(255)")]
        public string? Image { get; set; }

        [Required(ErrorMessage = $"Chức vụ {ValidationErrorsConstants.REQUIRED}")]
        [Column(TypeName = "nvarchar(255)")]
        public string Position { get; set; }
        
        // Gender
        [Required(ErrorMessage = $"Giới tính {ValidationErrorsConstants.REQUIRED}")]
        [Column(TypeName = "enum('Nam','Nữ','Khác')")]
        public string Gender { get; set; }

        // Cao đẳng, đại học, thạc sĩ, tiến sĩ
        [Column(TypeName = "nvarchar(255)")]
        public string? Degree { get; set; }

        // Phó giáo sư, giáo sư
        [Column(TypeName = "nvarchar(255)")]
        public string? Rank { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = $"Số điện thoại {ValidationErrorsConstants.INVALID}")]  
        [Column(TypeName = "nvarchar(255)")]
        [UniquePhone]
        public string? Phone { get; set; }

        [Column(TypeName = "text")]
        public string? ResearchArea { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [EmailAddress(ErrorMessage = $"Địa chỉ email {ValidationErrorsConstants.INVALID}")]
        [UniqueEmail]
        public string? Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Url(ErrorMessage = $"Url {ValidationErrorsConstants.INVALID}")]
        [UniqueLink]
        public string? LinkBlog { get; set; }

        [Column(TypeName = "text")]
        public string? Achievements { get; set; }
        
        public bool Status { get; set; }
        #endregion
        
        #region Relations
        public List<Subject>? Subjects { get; set; }
        
        public Account? Account { get; set; }
        
        public Department? Department { get; set; }
        
        public FacultyOffice? FacultyOffice { get; set; }
        #endregion

        #region NotMapped
        [FromForm]
        [NotMapped]
        [DataType(DataType.Upload)]
        [Validations.FileExtensions(".png,.jpg,.jpeg,.gif")]
        [FileMaxSize(5)]
        public IFormFile? ImageFile { get; set; }
            
        // Học hàm
        [NotMapped]
        public static List<string> DegreeList = new List<string>()
            { "Phó giáo sư", "Giáo sư", "GS.TS", "PGS.TS" };

        // Học vị
        [NotMapped]
        public static List<string> RankList = new List<string>()
            { "Cao đẳng", "Đại học", "Thạc sĩ", "Tiến sĩ" };

        [NotMapped]
        public static List<string> PositionList = new List<string>()
            { "Trưởng khoa", "Phó khoa", "Trưởng bộ môn", "Phó bộ môn", "Giảng viên", "Giáo vụ khoa" };
            
        [NotMapped]
        public static List<string> GenderList = new List<string>()
            { "Nam", "Nữ", "Khác"};
            
        [NotMapped]
        [ValidateSelectedSubjectIds]
        public List<int>? SelectedSubjectIdsList { get; set; }
            
        [NotMapped]
        public int? SelectedDepartmentId { get; set; }
        
        [NotMapped]
        public int? SelectedFacultyOfficeId { get; set; }
        #endregion

        #region Method
        public string[] SplitResearchArea()
        {
            if(ResearchArea is null)
            {
                return new string[0];
            }
            return ResearchArea.Split("/");
        }

        public string[] SplitAchivements()
        {
            if(Achievements is null)
            {
                return new string[0];
            }
            return Achievements.Split("/");
        }
        #endregion
    }
}