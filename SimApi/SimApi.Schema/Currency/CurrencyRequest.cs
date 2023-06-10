using SimApi.Base;

namespace SimApi.Schema;

public class CurrencyRequest : BaseRequest
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
}
