namespace SimApi.Data.Repository;

public interface IDapperTransactionRepository
{
    List<TransactionView> GetAll();
    TransactionView GetById(int id);
    List<TransactionView> GetByReferenceNumber(string referenceNumber);
    List<TransactionView> GetByCustomerId(int customerId);
    List<TransactionView> GetByAccountId(int accountId);
}
