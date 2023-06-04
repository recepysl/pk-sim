using SimApi.Base;
using SimApi.Data.Context;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<Category> CategoryRepository { get; private set; }
    public IDapperRepository<Account> DapperAccountRepository { get; private set; }
    public IDapperTransactionRepository DapperTransactionRepository { get; private set; }


    private readonly SimEfDbContext dbContext;
    private readonly SimDapperDbContext dapperDbContext;
    private bool disposed;

    public UnitOfWork(SimEfDbContext dbContext, SimDapperDbContext dapperDbContex)
    {
        this.dbContext = dbContext;
        this.dapperDbContext = dapperDbContex;

        CategoryRepository = new GenericRepository<Category>(dbContext);
        DapperAccountRepository = new DapperAccountRepository(dapperDbContext);
        DapperTransactionRepository = new DapperTransactionRepository(dapperDbContext);
    }

    public IDapperRepository<Entity> DapperRepository<Entity>() where Entity : BaseModel
    {
        return new DapperRepository<Entity>(dapperDbContext);
    }

    public IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel
    {
        return new GenericRepository<Entity>(dbContext);
    }
    public void Complete()
    {
        dbContext.SaveChanges();
    }

    public void CompleteWithTransaction()
    {
        using (var dbDcontextTransaction = dbContext.Database.BeginTransaction())
        {
            try
            {
                dbContext.SaveChanges();               
                dbDcontextTransaction.Commit();
            }
            catch (Exception ex)
            {
                // logging
                dbDcontextTransaction.Rollback();
            }         
        }
    }


    private void Clean(bool disposing)
    {
        if (!disposed)
        {
            if (disposing && dbContext is not null)
            {
                dbContext.Dispose();
            }
        }

        disposed = true;
        GC.SuppressFinalize(this);
    }
    public void Dispose()
    {
        Clean(true);
    }
}
