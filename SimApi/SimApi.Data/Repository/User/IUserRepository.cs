using SimApi.Data;

namespace SimApi.Data.Repository;

public interface IUserRepository : IGenericRepository<User>
{
    User GetByUsername(string name);
}
