using SimApi.Base;

namespace SimApi.Schema;

public class UserResponse : BaseResponse
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
