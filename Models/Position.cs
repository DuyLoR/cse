using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using CSE_website.Constants;


namespace CSE_website.Models
{
    public class Position
    {
         public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        [Required(ErrorMessage = $"Tên chức vụ {ValidationErrorsConstants.REQUIRED}")]
        
        public string Name { get; set; }
    }
}