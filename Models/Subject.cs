using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSE_website.Constants;
using CSE_website.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSE_website.Models
{
    public class Subject
    {
        public Subject()
        {
            this.Lecturers = new List<Lecturer>();
            this.Departments = new List<Department>();
           
        }
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = $"Tên môn học {ValidationErrorsConstants.REQUIRED}")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = $"Mô tả {ValidationErrorsConstants.REQUIRED}")]
        public string Decription { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Outline { get; set; }

        [Required(ErrorMessage = $"Số tín chỉ {ValidationErrorsConstants.REQUIRED}")]
        public int Credit { get; set; }



        public List<Lecturer>? Lecturers { get; set; }

        public List<Department>? Departments { get; set; }


        [NotMapped]
        [Required(ErrorMessage = $"Bộ môn {ValidationErrorsConstants.REQUIRED}")]

        public List<int>? SelectedDepartmentIdsList { get; set; }

       


        [NotMapped]
        public int? SelectedDepartmentId { get; set; }



        [FromForm]
        [NotMapped]
        [DataType(DataType.Upload)]
        [Validations.FileExtensions(".docx,.jpg,.jpeg,.gif")]
        [FileMaxSize(5)]
        public IFormFile? OutlineFile { get; set; }
    }
}
