using SimApi.Base;
using SimApi.Data.DbContext;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public class UnitOfWork : IUnitOfWork
{

    private readonly SimIdentiyDbContext dbContext;
    private bool disposed;

    public UnitOfWork(SimIdentiyDbContext dbContext)
    {
        this.dbContext = dbContext;       
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
