using SimApi.Data;

namespace SimApi.Data.Repository;

public interface ICategoryRepository : IGenericRepository<Category>
{
    IEnumerable<Category> FindByName (string name);
    int GetAllCount();
}
