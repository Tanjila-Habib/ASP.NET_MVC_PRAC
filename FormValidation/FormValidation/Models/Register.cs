using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FormValidation.Models
{
    public class Register
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s\.\-]+$", ErrorMessage = "Name can contain only letters and spaces")]
        public string Name { get; set; }
        [Required(ErrorMessage="Username is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can contain only letters and numbers, no spaces .")]
        public string Username { get; set; }
        [Required]
        [RegularExpression(@"^\d{2}-\d{5}-\d{1}$", ErrorMessage ="Id must be follow the format:XX-XXXXX-X")]
        public string ID { get; set; }
        [Required]
        [Custom(ErrorMessage = "The age must be greater than 18")]
        public DateTime DOB{ get; set; }
        [Required]
        [RegularExpression(@"^\d{2}-\d{5}-\d{2}@student\.aiub\.edu$",
    ErrorMessage = "Email must be in the format XX-XXXXX-XX@student.aiub.edu")]
        public string Email { get; set; }
    }
}