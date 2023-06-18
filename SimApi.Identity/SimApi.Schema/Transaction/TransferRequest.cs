using SimApi.Base;

namespace SimApi.Schema;

public class TransferRequest : BaseRequest
{
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
}
