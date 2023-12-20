using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GibJohn.Models
{
    // Defines all the properties which need to be stored for Notes which employees can submit, their constraints and their data types.
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters.", MinimumLength = 5)]
        public string Topic { get; set; }
        [Required]
        [StringLength(2000, ErrorMessage = "Must be between 5 and 250 characters.", MinimumLength = 5)]
        public string Notes { get; set; }
        [StringLength(150, ErrorMessage = "Must be between 15 and 150 characters.", MinimumLength = 15)]
        [AllowNull]
        public string? Video { get; set; }
    }
}