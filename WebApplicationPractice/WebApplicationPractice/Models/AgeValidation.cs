using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationPractice.Models
{
    public sealed class AgeValidation:ValidationAttribute
    {
        public override bool IsValid(object value)
        {  if(value == null) return false;
           var dob=DateTime.Parse(value.ToString());
            int age=DateTime.Now.Year-dob.Year;

            if(dob>DateTime.Now.AddYears(-age))
            {
                age--;
            }
            return age>=18;
        }
    }
}