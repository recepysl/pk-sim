using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data;

[Table("ApplicationUser", Schema = "dbo")]
public class ApplicationUser : IdentityUser
{
    public long? NationalIdNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
