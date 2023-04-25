using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSE_website.Models
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? Decription { get; set; }

        // public bool Status { get; set; }

        public List<Lecturer>? Lecturers { get; set; }
        
        public List<Subject>? Subjects { get; set; }
        
        public FacultyOffice? FacultyOffice { get; set; }
        [NotMapped]
        public List<int>? SelectedSubjectIdsList { get; set; }
            
        [NotMapped]
        public int? SelectedLecturerId { get; set; }
        
        [NotMapped]
        public int? SelectedFacultyOfficeId { get; set; }
       
    }
}
