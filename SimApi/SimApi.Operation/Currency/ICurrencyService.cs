using SimApi.Base;
using SimApi.Data;
using SimApi.Schema;

namespace SimApi.Operation;

public interface ICurrencyService : IBaseService<Currency,CurrencyRequest,CurrencyResponse>
{
    public ApiResponse Cache();    
}
