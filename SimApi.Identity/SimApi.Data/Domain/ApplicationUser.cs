using Microsoft.AspNetCore.Identity;

namespace SimApi.Data.Domain;

public class ApplicationUser : IdentityUser
{
    public long NationalIdNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
