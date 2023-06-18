using SimApi.Base;

namespace SimApi.Schema;

public class AccountRequest : BaseRequest
{
    public int CurrencyId { get; set; }
    public int CustomerId { get; set; }
    public int AccountNumber { get; set; }
    public string Name { get; set; }
    public DateTime OpenDate { get; set; }
    public decimal Balance { get; set; }
}
