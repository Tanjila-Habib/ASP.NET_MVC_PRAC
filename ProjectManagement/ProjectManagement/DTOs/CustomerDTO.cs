
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectManagement.CustomValidations;
namespace ProjectManagement.DTOs
{
    public class CustomerDTO
    {
        [Required]
        public int Id { get; set; }   // ✅ NOT required

        [Required]
        public string Name { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [ConfPassMatch]
        public string ConfPassWord { get; set; }
    }

}