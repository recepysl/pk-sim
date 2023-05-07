using SimApi.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimApi.Data.Domain;

[Table("User", Schema = "dbo")]
public class User : BaseModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public DateTime LastActivity { get; set; }
    public int PasswordRetryCount { get; set; }
    public int Status { get; set; }
}
