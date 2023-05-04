using SimpApi.Attribute;
using System.ComponentModel.DataAnnotations;

namespace SimpApi.Models;

public class Staff : IValidatableObject
{
    [Required]
    [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Invalid name")]
    public string Name { get; set; }

    [EmailAddress(ErrorMessage = "Invalid email")]
    [StringLength(maximumLength: 100, MinimumLength = 10, ErrorMessage = "Invalid email lenght")]
    public string Email { get; set; }

    [StringLength(maximumLength: 10, MinimumLength = 10, ErrorMessage = "Invalid phone")]
    [Phone(ErrorMessage = "Invalid phone")]
    public string Phone { get; set; }

    [Required]
    [MinSalary(20, 50)]
    public double HourlySalary { get; set; }


    [DOBAttribute]
    public DateTime DateOfBirth { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (DateTime.Today.AddYears(-10) > DateOfBirth)
        {
            yield return new ValidationResult("Invalid DOB");
        }
    }
}
