using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace GibJohn.Models
{
    // Defines all the properties which need to be stored for Courses which employees can submit, their constraints and their data types.
    public class Course
    {
        [Key]
        public int Id{ get; set; }
        [NotMapped]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }
        public string? ImagePath { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Name { get; set; }
        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Description { get; set; }
        [AllowNull]
        public string? ImageURL { get; set; }

        public Course() { }
    }
}