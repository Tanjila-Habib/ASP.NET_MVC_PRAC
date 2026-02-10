using System;
using System.ComponentModel.DataAnnotations;

public sealed class CustomAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var dob = DateTime.Parse(value.ToString());
        int age = DateTime.Now.Year - dob.Year;

        if (dob > DateTime.Now.AddYears(-age))
            age--;

        return age >= 18;
    }


}
