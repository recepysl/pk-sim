using System.ComponentModel.DataAnnotations;

namespace SimpApi.Models;

public class Employee
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
    [Range(minimum: 10, maximum: 50, ErrorMessage = "Invalid salary range")]
    public int HourlySalary { get; set; }
}
