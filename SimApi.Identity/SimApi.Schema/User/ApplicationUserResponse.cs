using SimApi.Data.Domain;

namespace SimApi.Schema;

public class ApplicationUserResponse : ApplicationUser
{
    public long NationalIdNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
