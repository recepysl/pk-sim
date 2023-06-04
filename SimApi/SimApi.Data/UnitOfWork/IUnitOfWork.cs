using SimApi.Base;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> CategoryRepository { get; }
    IDapperRepository<Account> DapperAccountRepository { get; }
    IDapperTransactionRepository DapperTransactionRepository { get; }

    IDapperRepository<Entity> DapperRepository<Entity>() where Entity : BaseModel;
    IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel;

    void Complete();
    void CompleteWithTransaction();
}
