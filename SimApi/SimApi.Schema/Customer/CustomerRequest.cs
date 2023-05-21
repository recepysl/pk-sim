using SimApi.Base;

namespace SimApi.Schema;

public class CustomerRequest : BaseRequest
{
    public int CustomerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }

    public List<AccountRequest> Accounts { get; set; } = new List<AccountRequest>();
}
