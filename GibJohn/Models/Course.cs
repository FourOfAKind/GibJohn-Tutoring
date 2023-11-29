﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace GibJohn.Models
{
    public class Course
    {
        [Key]
        public int Id{ get; set; }
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
