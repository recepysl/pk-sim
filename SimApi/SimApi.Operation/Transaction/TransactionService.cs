using AutoMapper;
using LinqKit;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;
using System.Linq.Expressions;

namespace SimApi.Operation;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IAccountService accountService;
    private readonly IRedisService redisService;

    public TransactionService(IUnitOfWork unitOfWork, IRedisService redisService, IMapper mapper, IAccountService accountService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.redisService = redisService;
        this.accountService = accountService;
    }

    public ApiResponse<List<TransactionResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.Repository<Transaction>().GetAll();
            var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
            return new ApiResponse<List<TransactionResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<TransactionResponse>>(ex.Message);
        }
    }

    public ApiResponse<List<TransactionResponse>> GetByParameter(int accountId, int customerId, decimal amount, string description)
    {
        var predicate = GetExpression(accountId, customerId, amount, description);
        var list = unitOfWork.Repository<Transaction>().Where(predicate).ToList();

        var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(list);
        return new ApiResponse<List<TransactionResponse>>(mapped);
    }

    public ApiResponse<List<TransactionResponse>> GetByReference(string referenceNumber)
    {
        try
        {
            var entityList = unitOfWork.Repository<Transaction>().Where(x => x.ReferenceNumber == referenceNumber).ToList();
            var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
            return new ApiResponse<List<TransactionResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<TransactionResponse>>(ex.Message);
        }
    }

    public ApiResponse<TransactionResponse> GetById(int id)
    {
        try
        {
            var entity = unitOfWork.Repository<Transaction>().GetById(id);
            if (entity is null)
            {
                return new ApiResponse<TransactionResponse>("Record not found");
            }

            var mapped = mapper.Map<Transaction, TransactionResponse>(entity);
            return new ApiResponse<TransactionResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<TransactionResponse>(ex.Message);
        }
    }

    public ApiResponse<CashResponse> Withdraw(CashRequest request)
    {
        if (request is null)
        {
            return new ApiResponse<CashResponse>("Invalid Request");
        }

        var accountResponse = accountService.GetById(request.AccountId);
        if (!accountResponse.Success || accountResponse.Response is null)
        {
            return new ApiResponse<CashResponse>(accountResponse.Message);
        }
        var account = accountResponse.Response;


        var currencyResponse = GetCurrency(account.CurrencyId);
        if (!currencyResponse.Success || currencyResponse.Response is null)
        {
            return new ApiResponse<CashResponse>(currencyResponse.Message);
        }

        var balanceResponse = accountService.Balance(request.AccountId, request.Amount, TransactionDirection.Withdraw);
        if (!balanceResponse.Success)
        {
            return new ApiResponse<CashResponse>(balanceResponse.Message);
        }

        var referenceNumber = ReferenceNumberGenerator.Get();

        Transaction transaction = new();
        transaction.TransactionDate = DateTime.UtcNow;
        transaction.TransactionCode = TransactionCode.Withdraw;
        transaction.AccountId = account.Id;
        transaction.Amount = request.Amount;
        transaction.Direction = (byte)TransactionDirection.Withdraw;
        transaction.ReferenceNumber = referenceNumber;
        transaction.Description = request.Description;
        transaction.CurrencyCode = currencyResponse.Response.Code;

        unitOfWork.Repository<Transaction>().Insert(transaction);
        unitOfWork.Complete();

        CashResponse response = new CashResponse
        {
            Id = transaction.Id,
            ReferenceNumber = referenceNumber
        };

        return new ApiResponse<CashResponse>(response);
    }



    public ApiResponse<CashResponse> Deposit(CashRequest request)
    {
        if (request is null)
        {
            return new ApiResponse<CashResponse>("Invalid Request");
        }

        var accountResponse = accountService.GetById(request.AccountId);
        if (!accountResponse.Success || accountResponse.Response is null)
        {
            return new ApiResponse<CashResponse>(accountResponse.Message);
        }
        var account = accountResponse.Response;

        var currencyResponse = GetCurrency(account.CurrencyId);
        if (!currencyResponse.Success || currencyResponse.Response is null)
        {
            return new ApiResponse<CashResponse>(currencyResponse.Message);
        }

        var balanceResponse = accountService.Balance(request.AccountId, request.Amount, TransactionDirection.Deposit);
        if (!balanceResponse.Success)
        {
            return new ApiResponse<CashResponse>(balanceResponse.Message);
        }

        var referenceNumber = ReferenceNumberGenerator.Get();

        Transaction transaction = new();
        transaction.TransactionDate = DateTime.UtcNow;
        transaction.TransactionCode = TransactionCode.Deposit;
        transaction.AccountId = account.Id;
        transaction.Amount = request.Amount;
        transaction.Direction = (byte)TransactionDirection.Deposit;
        transaction.ReferenceNumber = referenceNumber;
        transaction.Description = request.Description;
        transaction.CurrencyCode = currencyResponse.Response.Code;

        unitOfWork.Repository<Transaction>().Insert(transaction);
        unitOfWork.Complete();

        CashResponse response = new CashResponse
        {
            Id = transaction.Id,
            ReferenceNumber = referenceNumber
        };

        return new ApiResponse<CashResponse>(response);
    }

    public ApiResponse<TransferResponse> Transfer(TransferRequest request)
    {
        if (request is null)
        {
            return new ApiResponse<TransferResponse>("Invalid Request");
        }

        if (request.FromAccountId == request.ToAccountId)
        {
            return new ApiResponse<TransferResponse>("Invalid Account");
        }

        if (request.Amount <= 0)
        {
            return new ApiResponse<TransferResponse>("Invalid Amount");
        }

        var fromAccountResponse = accountService.GetById(request.FromAccountId);
        if (!fromAccountResponse.Success || fromAccountResponse.Response is null)
        {
            return new ApiResponse<TransferResponse>(fromAccountResponse.Message);
        }
        var fromAccount = fromAccountResponse.Response;
        var currencyResponseFrom = GetCurrency(fromAccount.CurrencyId);
        if (!currencyResponseFrom.Success || currencyResponseFrom.Response is null)
        {
            return new ApiResponse<TransferResponse>(currencyResponseFrom.Message);
        }
        var fromBalanceResponse = accountService.Balance(request.FromAccountId, request.Amount, TransactionDirection.Withdraw);
        if (!fromBalanceResponse.Success)
        {
            return new ApiResponse<TransferResponse>(fromBalanceResponse.Message);
        }
       

        var toAccountResponse = accountService.GetById(request.ToAccountId);
        if (!toAccountResponse.Success || toAccountResponse.Response is null)
        {
            return new ApiResponse<TransferResponse>(toAccountResponse.Message);
        }
        var toAccount = toAccountResponse.Response;
        var currencyResponseTo = GetCurrency(toAccount.CurrencyId);
        if (!currencyResponseTo.Success || currencyResponseTo.Response is null)
        {
            return new ApiResponse<TransferResponse>(currencyResponseTo.Message);
        }
        var toBalanceResponse = accountService.Balance(request.ToAccountId, request.Amount, TransactionDirection.Deposit);
        if (!toBalanceResponse.Success)
        {
            return new ApiResponse<TransferResponse>(toBalanceResponse.Message);
        }

        if (currencyResponseFrom.Response.Code != currencyResponseTo.Response.Code)
        {
            return new ApiResponse<TransferResponse>("Invalid account currency.");
        }

        bool isSameCustomer = fromAccount.CustomerId == toAccount.CustomerId;
        string refenceNumber = ReferenceNumberGenerator.Get();

        Transaction transactionTo = new();
        transactionTo.TransactionDate = DateTime.UtcNow;
        transactionTo.TransactionCode = isSameCustomer ? TransactionCode.TransferToMyself : TransactionCode.TransferToOthers;
        transactionTo.AccountId = toAccount.Id;
        transactionTo.Amount = request.Amount;
        transactionTo.Direction = (byte)TransactionDirection.Deposit;
        transactionTo.ReferenceNumber = refenceNumber;
        transactionTo.Description = request.Description;
        transactionTo.CurrencyCode = currencyResponseTo.Response.Code;
        unitOfWork.Repository<Transaction>().Insert(transactionTo);

        Transaction transactionFrom = new();
        transactionFrom.TransactionDate = DateTime.UtcNow;
        transactionFrom.TransactionCode = isSameCustomer ? TransactionCode.TransferToMyself : TransactionCode.TransferToOthers;
        transactionFrom.AccountId = fromAccount.Id;
        transactionFrom.Amount = request.Amount;
        transactionFrom.Direction = (byte)TransactionDirection.Withdraw;
        transactionFrom.ReferenceNumber = refenceNumber;
        transactionFrom.Description = request.Description;
        transactionFrom.CurrencyCode = currencyResponseFrom.Response.Code;
        unitOfWork.Repository<Transaction>().Insert(transactionFrom);

        unitOfWork.Complete();

        TransferResponse response = new TransferResponse()
        {
            ReferenceNumber = refenceNumber
        };

        return new ApiResponse<TransferResponse>(response);
    }
   
    public ApiResponse<List<TransactionViewResponse>> Report1()
    {
        try
        {
            var entityList = unitOfWork.Repository<TransactionView>().GetAll();
            var mapped = mapper.Map<List<TransactionView>, List<TransactionViewResponse>>(entityList);
            return new ApiResponse<List<TransactionViewResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<TransactionViewResponse>>(ex.Message);
        }
    }
    public ApiResponse<List<TransactionViewResponse>> Report2()
    {
        try
        {
            var list = (from tran in unitOfWork.Repository<Transaction>().GetAsQueryable()
                        join acc in unitOfWork.Repository<Account>().GetAsQueryable() on tran.AccountId equals acc.Id
                        join cus in unitOfWork.Repository<Customer>().GetAsQueryable() on acc.CustomerId equals cus.Id
                        where tran.TransactionCode == TransactionCode.Deposit
                        select new TransactionViewResponse
                        {
                            AccountId = acc.Id,
                            Amount = tran.Amount,
                            Description = tran.Description,
                            Direction = tran.Direction,
                            Id = tran.Id,
                            ReferenceNumber = tran.ReferenceNumber,
                            TransactionCode = tran.TransactionCode,
                            TransactionDate = tran.TransactionDate,
                            AccountName = acc.Name,
                            AccountNumber = acc.AccountNumber,
                            CustomerId = acc.CustomerId,
                            CustomerNumber = cus.CustomerNumber,
                            FirstName = cus.FirstName,
                            LastName = cus.LastName,
                            CurrencyCode = tran.CurrencyCode
                        }).ToList();

            return new ApiResponse<List<TransactionViewResponse>>(list);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<TransactionViewResponse>>(ex.Message);
        }
    }



    private Expression<Func<Transaction, bool>> GetExpression(int accountId, int customerId, decimal amount, string description)
    {
        var predicate = PredicateBuilder.New<Transaction>(true);
        if (!string.IsNullOrEmpty(description))
            predicate.And(x => x.Description.StartsWith(description));
        if (accountId > 0)
            predicate.And(x => x.AccountId == accountId);
        if (customerId > 0)
            predicate.And(x => x.Account.CustomerId == customerId);
        if (amount > 0)
            predicate.And(x => x.Amount == amount);

        return predicate;
    }

    private ApiResponse<CurrencyResponse> GetCurrency(int currencyId)
    {
        var cashe = redisService.GetDynamic<List<CurrencyResponse>>(RedisKeys.CurrencyList);
        if (cashe == null)
        {
            return new ApiResponse<CurrencyResponse>("Invalid currency cashe");
        }
        var currency = cashe.Where(x => x.Id == currencyId).FirstOrDefault();
        if (currency == null)
        {
            return new ApiResponse<CurrencyResponse>("Invalid currency cashe");
        }
        if (!currency.IsActive)
        {
            return new ApiResponse<CurrencyResponse>("Currency is not supported.");
        }

        return new ApiResponse<CurrencyResponse>(currency);
    }

}


