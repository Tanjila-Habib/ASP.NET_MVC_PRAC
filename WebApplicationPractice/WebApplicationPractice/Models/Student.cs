using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationPractice.Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(100), MinLength(5)]
        [RegularExpression(@"^[A-Za-z\\s]+$", ErrorMessage = "Name cannot contain numbers or special characters.")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"^\d{2}-\d{5}-\d{1}$", ErrorMessage = "Id must be in XX-XXXXX-X format.")]
        public string StudentId { get; set; }
        [Required]
        [RegularExpression(@"^[0-9\-]+@student.aiub.edu$", ErrorMessage = "Email must be in XX-XXXXX-X@student.aiub.edu format")]
        public string Email { get; set; }
        [Required]
        [AgeValidation(ErrorMessage = "Age must be 18 ")]

        public DateTime Age { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
}