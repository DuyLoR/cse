using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using CSE_website.Validations;

namespace CSE_website.Models
{
    public class SendMailModel
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Password { get; set; }
        
        [NotMapped]
        public string? ConfirmPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [Column(TypeName = "nvarchar(255)")]
        public string? ResetToken{ get; set; }
        
        public DateTime? ResetTokenExpiration { get; set; }
        public bool Status { get; set; }
        public Permission? Permission { get; set; }
    }
}
