using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CSE_website.Constants;
using CSE_website.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSE_website.Models
{
    public class Partner
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = $"Tên đơn vị {ValidationErrorsConstants.REQUIRED}")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Logo { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Link { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = $"Tiêu đề {ValidationErrorsConstants.REQUIRED}")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Description { get; set; }

        
        [Column(TypeName = "nvarchar(255)")]
        public string? Phone { get; set; }
        
        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = $"Email {ValidationErrorsConstants.REQUIRED}")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = $"Thứ tự {ValidationErrorsConstants.REQUIRED}")]
        public int Order { get; set; }

        
        [Required(ErrorMessage = $"Loại đối tác {ValidationErrorsConstants.REQUIRED}")]
        [Column(TypeName = "enum('Đồng','Bạc','Vàng')")]
        public string Type { get; set; }
        public bool Status { get; set; }
        public Category? Category { get; set; }
        //notmap
        [NotMapped]
       
        public int? SelectedCategoryId { get; set; }
         [NotMapped]
        public static List<string> TypeList = new List<string>()
            { "Đồng", "Bạc", "Vàng"};

      
        [FromForm]
        [NotMapped]
        [DataType(DataType.Upload)]
        [Validations.FileExtensions(".png,.jpg,.jpeg,.gif")]
        [FileMaxSize(5)]
        public IFormFile? ImageFile { get; set; }
        
    }
}
