using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using CSE_website.Validations;
using Microsoft.AspNetCore.Mvc;
using Mysqlx;

namespace CSE_website.Models
{
    public class FacultyOffice
    {
        public FacultyOffice()
        {
            this.Lecturers = new List<Lecturer>();
            this.Departments = new List<Department>();
        }
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Phone { get; set; }

        [FromForm]
        [NotMapped]
        [DataType(DataType.Upload)]
        // [FileExtensions("png,jpg,jpeg,gif", ErrorMessage = "Ảnh phải có đuôi png,jpg,jpeg,gif")]
        [FileMaxSize(2)]
        public IFormFile? FileUpload { get; set; }
        
        [Column(TypeName = "nvarchar(255)")]
        public string Logo { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Address { get; set; }
        
        public List<Lecturer>? Lecturers { get; set; }
        
        public List<Department>? Departments { get; set; }
    }
}
