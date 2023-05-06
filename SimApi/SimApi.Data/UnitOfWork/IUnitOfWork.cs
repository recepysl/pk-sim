using SimApi.Data.Domain;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Entity> GetRepository<Entity>() where Entity : class;
    // IGenericRepository<Category> CategoryRepository { get; }
    // IGenericRepository<Product> ProductRepository { get; }

    void Complete();
}
