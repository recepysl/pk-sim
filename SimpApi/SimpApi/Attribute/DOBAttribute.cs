using SimpApi.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpApi.Attribute;

public class DOBAttribute : ValidationAttribute
{

    public DOBAttribute()
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var staff = (Staff)validationContext.ObjectInstance;
        bool isOlderThan18Years = staff.DateOfBirth <= DateTime.Today.AddYears(-18);

        if (isOlderThan18Years)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Invalid date of birth");
        }
    }
}
