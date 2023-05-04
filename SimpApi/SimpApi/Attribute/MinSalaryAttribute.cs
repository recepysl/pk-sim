using SimpApi.Models;
using System.ComponentModel.DataAnnotations;

namespace SimpApi.Attribute;

public class MinSalaryAttribute : ValidationAttribute
{
    public double MinSalary { get; }
    public double MaxSalary { get; }

    public MinSalaryAttribute(double minSalary, double maxSalary)
    {
        this.MinSalary = minSalary;
        this.MaxSalary = maxSalary;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var staff = (Staff)validationContext.ObjectInstance;
        bool isOlderthen18Years = staff.DateOfBirth <= DateTime.Today.AddYears(-18);

        double salary = staff.HourlySalary;
        bool isValidSalary = isOlderthen18Years ? salary <= MaxSalary : salary <= MinSalary;

        if (isValidSalary)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Invalid salary range");
        }
    }
}
