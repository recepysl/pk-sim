using Microsoft.AspNetCore.Identity;

namespace WebApp;

public class ApplicationUser : IdentityUser
{
    public string Address { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}
