using SimApi.Base;

namespace SimApi.Schema;

public class AccountResponse : BaseResponse
{
    public int CustomerId { get; set; }
    public int AccountNumber { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }
    public decimal Balance { get; set; }
    public bool IsValid { get; set; }

    public List<TransactionResponse> Transactions { get; set; }
}
