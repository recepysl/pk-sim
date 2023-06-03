using SimApi.Base;

namespace SimApi.Schema;

public class TransactionResponse: BaseResponse
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public byte Direction { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public string ReferenceNumber { get; set; }
    public string TransactionCode { get; set; }
}
