using SimApi.Data.Context;
using SimApi.Data;

namespace SimApi.Data.Repository;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(SimEfDbContext context) : base(context)
    {

    }

    public IEnumerable<Category> FindByName(string name)
    {
        var list = dbContext.Set<Category>().Where(c => c.Name.Contains(name)).ToList();
        return list;
    }

    public int GetAllCount()
    {
        return dbContext.Set<Category>().Count();
    }
}
