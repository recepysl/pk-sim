using SimApi.Base;

namespace SimApi.Data.Repository;

public interface IDapperRepository<Entity> where Entity : BaseModel
{
    List<Entity> GetAll();
    List<Entity> Filter(string sql);
    Entity GetById(int id);
    void Insert(Entity entity);
    void Update(Entity entity);
    void DeleteById(int id);
}
