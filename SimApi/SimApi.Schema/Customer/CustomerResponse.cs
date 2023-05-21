using SimApi.Base;

namespace SimApi.Schema;

public class CustomerResponse : BaseResponse
{
    public int CustomerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool IsValid { get; set; }

    public List<AccountResponse> Accounts { get; set; }
}
