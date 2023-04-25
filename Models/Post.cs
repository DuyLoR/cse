using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_website.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Image { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(255)")]
        public string? Type { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Tag { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Source { get; set; }
        public bool Status { get; set; }
        public DateTime? TimeStartEvent { get; set; }
        public DateTime? TimeEndEvent { get; set; }
        
        public Category? Category { get; set; }

    }
}
