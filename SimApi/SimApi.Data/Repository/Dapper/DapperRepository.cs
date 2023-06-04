using SimApi.Base;
using SimApi.Data.Context;

namespace SimApi.Data.Repository;

public class DapperRepository<Entity> : IDapperRepository<Entity> where Entity : BaseModel
{
    protected readonly SimDapperDbContext dbContext;
    private bool disposed;

    public DapperRepository(SimDapperDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public List<Entity> Filter(string sql)
    {
        throw new NotImplementedException();
    }

    public List<Entity> GetAll()
    {
        throw new NotImplementedException();
    }

    public Entity GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Insert(Entity entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Entity entity)
    {
        throw new NotImplementedException();
    }
}
