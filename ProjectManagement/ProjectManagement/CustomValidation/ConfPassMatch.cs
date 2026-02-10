using ProjectManagement.DTOs;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ProjectManagement.CustomValidations
{
    public class ConfPassMatch : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as CustomerDTO;

            if (obj == null)
                return ValidationResult.Success;

            // Let [Required] handle empty checks
            if (string.IsNullOrWhiteSpace(obj.Password) ||
                string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return ValidationResult.Success;
            }

            if (obj.Password != value.ToString())
            {
                return new ValidationResult("Password and Confirm Password do not match.");
            }

            return ValidationResult.Success;
        }
    }
}
