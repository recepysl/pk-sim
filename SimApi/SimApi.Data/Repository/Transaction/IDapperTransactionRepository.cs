namespace SimApi.Data.Repository;

public interface IDapperTransactionRepository
{
    List<Transaction> GetAll();
    Transaction GetById(int id);
    List<Transaction> GetByReferenceNumber(string referenceNumber);
    List<Transaction> GetByCustomerId(int customerId);
    List<Transaction> GetByAccountId(int accountId);
}
