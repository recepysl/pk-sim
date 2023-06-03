using SimApi.Base;

namespace SimApi.Schema
{
    public class CashRequest : BaseRequest
    {
        public int AccountId { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
