using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GibJohn.Models
{
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
        [StringLength(250, ErrorMessage = "Must be between 5 and 250 characters.", MinimumLength = 5)]
        public string Notes { get; set; }
    }
}
