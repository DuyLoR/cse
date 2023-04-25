using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_website.Models
{
    public class Permission
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]  
        public string Name { get; set; }
    }
}
