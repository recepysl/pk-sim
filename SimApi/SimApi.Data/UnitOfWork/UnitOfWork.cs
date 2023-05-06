using SimApi.Data.Context;
using SimApi.Data.Domain;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<Category> CategoryRepository { get; private set; }
    public IGenericRepository<Product> ProductRepository { get; private set; }

    private readonly SimDbContext dbContext;

    public UnitOfWork(SimDbContext dbContext)
    {
        this.dbContext = dbContext;


        CategoryRepository = new GenericRepository<Category>(dbContext);
        ProductRepository = new GenericRepository<Product>(dbContext);
    }
    public void Complete()
    {
        dbContext.SaveChanges();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
