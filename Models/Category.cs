using CSE_website.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_website.Models
{
    [BindProperties]
    public class Category
    {
        public Category()
        {
            this.ChildCategoriesList = new List<Category>(){};
        }
        public int Id { get; set; }

        [Required(ErrorMessage = $"Tiêu đề {ValidationErrorsConstants.REQUIRED}")]
        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Description { get; set; }

        public int Order { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Level { get; set; }

        [Column(TypeName = "text")]
        public string? Link { get; set; }

        [Column(TypeName = "enum('_self','_blank')")] 
        // Default: 'Không'
        public string Target { get; set; } = "_self";

        [Column(TypeName = "enum(1,2)")] 
        // (Danh mục chính, Danh mục phụ) (Default: 1)
        public int Type { get; set; } = 1;

        public bool Status { get; set; } = true;

        #region NotMapped
        [NotMapped]
        public int? SelectedLecturerId { get; set; }
        [NotMapped]
        public int? SelectedCategoryId { get; set; }
        
        public static Dictionary<string, string> TargetTypesList = new Dictionary<string, string>()
        { 
            {"_self", "Trang hiện tại"},
            {"_blank", "Trang mới"},
        };

        public static Dictionary<int, string> TypesList = new Dictionary<int, string>()
        { 
            {1, "Danh mục chính"},
            {2, "Danh mục phụ"},
        };
        #endregion

        #region Relations
        public Lecturer? Lecturer { get; set; }
        public Category? CategoryParent { get; set; }
        public List<Category> ChildCategoriesList { get; set; }
        #endregion
    }
}
