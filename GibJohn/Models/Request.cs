using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace GibJohn.Models
{
    public class Request
    {
        // Defines all the properties which need to be stored for Requested Courses which teachers can submit, their constraints and their data types.
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters.", MinimumLength = 5)]
        public string Title { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters.", MinimumLength = 5)]
        public string CourseLevel { get; set; }
        [Required]
        [StringLength(25, ErrorMessage = "Must be between 2 and 25 characters.", MinimumLength = 2)]
        public string ExamBoard {  get; set; }
        [AllowNull]
        public string? Comment { get; set; }
    }
}