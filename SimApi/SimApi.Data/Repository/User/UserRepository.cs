using SimApi.Data.Context;
using SimApi.Data.Domain;

namespace SimApi.Data.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(SimDbContext context) : base(context)
    {

    }

}
