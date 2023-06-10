using SimApi.Base;

namespace SimApi.Schema;

public class UserLogResponse : BaseResponse
{
    public string UserName { get; set; }
    public DateTime TransactionDate { get; set; }
    public string LogType { get; set; }
}
