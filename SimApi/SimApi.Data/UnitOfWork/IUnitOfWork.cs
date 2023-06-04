using SimApi.Base;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> CategoryRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<User> UserRepository { get; }
    IGenericRepository<UserLog> UserLogRepository { get; }
    IGenericRepository<Account> AccountRepository { get; }
    IGenericRepository<Transaction> TransactionRepository { get; }


    IDapperRepository<Account> DapperAccountRepository { get; }
    IDapperTransactionRepository DapperTransactionRepository { get; }

    IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel;

    void Complete();
    void CompleteWithTransaction();
}
