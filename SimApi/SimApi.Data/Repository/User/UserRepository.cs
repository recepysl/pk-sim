using SimApi.Data.Context;
using SimApi.Data.Domain;

namespace SimApi.Data.Repository;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(SimDbContext context) : base(context)
    {

    }

    public User GetByUsername(string name)
    {
        return dbContext.Set<User>().Where(x => x.UserName == name).FirstOrDefault();
    }
}
