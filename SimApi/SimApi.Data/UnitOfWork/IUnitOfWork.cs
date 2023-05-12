using SimApi.Data.Domain;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public interface IUnitOfWork : IDisposable
{
    //IGenericRepository<Category> CategoryRepository { get; }
    //IGenericRepository<Product> ProductRepository { get; }
    IGenericRepository<Entity> GetRepository<Entity>() where Entity : class;
    void Complete();
    void CompleteWithTransaction();
}
