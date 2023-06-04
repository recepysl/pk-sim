using Microsoft.AspNetCore.Mvc;
using SimApi.Base;
using SimApi.Operation;
using SimApi.Schema;

namespace SimApi.Service;

[EnableMiddlewareLogger]
[ResponseGuid]
[Route("simapi/v1/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService transactionService;
    public TransactionController(ITransactionService transactionService)
    {
        this.transactionService = transactionService;
    }

    [HttpGet]
    public ApiResponse<List<TransactionResponse>> GetAll()
    {
        var transactionList = transactionService.GetAll();
        return transactionList;
    }

    [HttpGet("{id}")]
    public ApiResponse<TransactionResponse> GetById(int id)
    {
        var transaction = transactionService.GetById(id);
        return transaction;
    }

    [HttpGet("ByReferenceNumber")]
    public ApiResponse<List<TransactionResponse>> GetByReferenceNumber([FromQuery] string referenceNumber)
    {
        var transactions = transactionService.GetByReference(referenceNumber);
        return transactions;
    }

    [HttpGet("ByParameter")]
    public ApiResponse<List<TransactionResponse>> GetByParameter(
        [FromQuery] int? accountId, [FromQuery] int? customerId,[FromQuery] decimal? amount,
        [FromQuery] string? description)
    {
        var transactions = transactionService.GetByParameter(accountId ?? 0,customerId ?? 0,amount ?? 0,description);
        return transactions;
    }

    [HttpPost("Withdraw")]
    public ApiResponse<CashResponse> Withdraw([FromBody] CashRequest request)
    {
        var response = transactionService.Withdraw(request);
        return response;
    }

    [HttpPost("Deposit")]
    public ApiResponse<CashResponse> Deposit([FromBody] CashRequest request)
    {
        var response = transactionService.Deposit(request);
        return response;
    }

    [HttpPost("Transfer")]
    public ApiResponse<TransferResponse> Transfer([FromBody] TransferRequest request)
    {
        var response = transactionService.Transfer(request);
        return response;
    }


}
