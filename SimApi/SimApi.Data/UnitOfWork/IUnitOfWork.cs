using SimApi.Data.Domain;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Category> CategoryRepository { get; }
    IGenericRepository<Product> ProductRepository { get; }

    void Complete();
    void CompleteWithTransaction();
}
