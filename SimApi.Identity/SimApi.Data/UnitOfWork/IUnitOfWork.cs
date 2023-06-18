using SimApi.Base;
using SimApi.Data.Repository;

namespace SimApi.Data.Uow;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Entity> Repository<Entity>() where Entity : BaseModel;
    void Complete();
    void CompleteWithTransaction();
}
