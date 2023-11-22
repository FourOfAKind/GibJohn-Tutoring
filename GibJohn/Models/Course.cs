using System.ComponentModel.DataAnnotations;

namespace GibJohn.Models
{
    public class Course
    {

        public int CourseID { get; set; }
        [StringLength(50, ErrorMessage = "Must be between 2 and 50 characters", MinimumLength = 2)]
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ImageURL { get; set; }

        public Course() { }
    }
}
