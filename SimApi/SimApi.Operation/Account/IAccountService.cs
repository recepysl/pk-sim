using SimApi.Base;
using SimApi.Data;
using SimApi.Schema;

namespace SimApi.Operation;

public interface IAccountService : IBaseService<Account, AccountRequest, AccountResponse>
{
    ApiResponse<List<AccountResponse>> ByCustomerId(int customerId);
    ApiResponse Balance(int accountId,decimal amount, TransactionDirection direction);
}
