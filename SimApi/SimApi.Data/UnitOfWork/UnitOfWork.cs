using SimApi.Data.Context;
using SimApi.Data;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<Category> CategoryRepository { get; private set; }
    public IGenericRepository<Product> ProductRepository { get; private set; }
    public IGenericRepository<User> UserRepository { get; private set; }
    public IGenericRepository<UserLog> UserLogRepository { get; private set; }


    private readonly SimDbContext dbContext;
    private bool disposed;

    public UnitOfWork(SimDbContext dbContext)
    {
        this.dbContext = dbContext;

        CategoryRepository = new GenericRepository<Category>(dbContext);
        ProductRepository = new GenericRepository<Product>(dbContext);
        UserRepository = new GenericRepository<User>(dbContext);
        UserLogRepository = new GenericRepository<UserLog>(dbContext);
    }

    public IGenericRepository<Entity> Repository<Entity>() where Entity : class
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
            if (disposing)
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
