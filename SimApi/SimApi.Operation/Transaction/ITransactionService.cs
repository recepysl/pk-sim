﻿
using SimApi.Base;
using SimApi.Schema;

namespace SimApi.Operation;

public interface ITransactionService
{
    ApiResponse<List<TransactionResponse>> GetAll();
    ApiResponse<TransactionResponse> GetById(int id);
    ApiResponse<TransferResponse> Transfer(TransferRequest request);
    ApiResponse<CashResponse> Deposit(CashRequest request);
    ApiResponse<CashResponse> Withdraw(CashRequest request);
}