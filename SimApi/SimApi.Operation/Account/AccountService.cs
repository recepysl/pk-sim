using AutoMapper;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation;

public class AccountService : BaseService<Account, AccountRequest, AccountResponse>, IAccountService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public AccountService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public ApiResponse<List<AccountResponse>> ByCustomerId(int customerId)
    {
        if (customerId == 0)
            return new ApiResponse<List<AccountResponse>>("Invalid Customer ID");

        try
        {
            var entityList = unitOfWork.Repository<Account>().Where(x => x.CustomerId == customerId).ToList();
            var mapped = mapper.Map<List<Account>, List<AccountResponse>>(entityList);
            return new ApiResponse<List<AccountResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<AccountResponse>>(ex.Message);
        }
    }
}
