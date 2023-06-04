using SimApi.Base;

namespace SimApi.Schema;

public class TransactionViewResponse : BaseResponse
{
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public byte Direction { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Description { get; set; }
    public string ReferenceNumber { get; set; }
    public string TransactionCode { get; set; }

    public int CustomerId { get; set; }
    public int AccountNumber { get; set; }
    public string AccountName { get; set; }

    public int CustomerNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
