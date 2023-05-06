using SimApi.Base;

namespace SimApi.Data.Domain;

public class Category : BaseModel
{
    public string Name { get; set; }
    public int Order { get; set; }

}
