using AutoMapper;
using SimApi.Base;
using SimApi.Data;
using SimApi.Data.Uow;
using SimApi.Schema;

namespace SimApi.Operation;

public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IAccountService accountService;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper,IAccountService accountService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.accountService = accountService;
    }

    public ApiResponse<List<TransactionResponse>> GetAll()
    {
        try
        {
            var entityList = unitOfWork.Repository<Transaction>().GetAllWithInclude("Accounts");
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
            var entity = unitOfWork.Repository<Transaction>().GetByIdWithInclude(id, "Accounts");
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

        var referenceNumber = ReferenceNumberGenerator.Get();

        Transaction transaction = new();
        transaction.TransactionDate = DateTime.Now;
        transaction.TransactionCode = TransactionCode.Withdraw;
        transaction.AccountId = account.Id;
        transaction.Amount = request.Amount;
        transaction.Direction = 2;
        transaction.ReferenceNumber = referenceNumber;
        transaction.Description = request.Description;

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

        var referenceNumber = ReferenceNumberGenerator.Get();

        Transaction transaction = new();
        transaction.TransactionDate = DateTime.Now;
        transaction.TransactionCode = TransactionCode.Deposit;
        transaction.AccountId = account.Id;
        transaction.Amount = request.Amount;
        transaction.Direction = 1;
        transaction.ReferenceNumber = referenceNumber;
        transaction.Description = request.Description;

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
            return new ApiResponse<TransferResponse>("Invalid Accounts");
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

        var toAccountResponse = accountService.GetById(request.ToAccountId);
        if (!toAccountResponse.Success || toAccountResponse.Response is null)
        {
            return new ApiResponse<TransferResponse>(toAccountResponse.Message);
        }
        var toAccount = toAccountResponse.Response;

        bool isSameCustomer = fromAccount.CustomerId == toAccount.CustomerId;
        string refenceNumber = ReferenceNumberGenerator.Get();

        Transaction transactionTo = new();
        transactionTo.TransactionDate = DateTime.Now;
        transactionTo.TransactionCode = isSameCustomer ? TransactionCode.TransferToMyself : TransactionCode.TransferToOthers;
        transactionTo.AccountId = toAccount.Id;
        transactionTo.Amount = request.Amount;
        transactionTo.Direction = 1;
        transactionTo.ReferenceNumber = refenceNumber;
        transactionTo.Description = request.Description;
        unitOfWork.Repository<Transaction>().Insert(transactionTo);


        Transaction transactionFrom = new();
        transactionFrom.TransactionDate = DateTime.Now;
        transactionFrom.TransactionCode = isSameCustomer ? TransactionCode.TransferToMyself : TransactionCode.TransferToOthers;
        transactionFrom.AccountId = fromAccount.Id;
        transactionFrom.Amount = request.Amount;
        transactionFrom.Direction = 2;
        transactionFrom.ReferenceNumber = refenceNumber;
        transactionFrom.Description = request.Description;
        unitOfWork.Repository<Transaction>().Insert(transactionFrom);

        unitOfWork.Complete();

        TransferResponse response = new TransferResponse()
        {
            ReferenceNumber = refenceNumber
        };

        return new ApiResponse<TransferResponse>(response);
    }


    
    
}
